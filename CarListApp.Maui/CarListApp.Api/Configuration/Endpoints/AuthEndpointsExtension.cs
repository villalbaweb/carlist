using CarListApp.Api.Core.Dtos;
using CarListApp.Api.Core.Settings;
using CarListApp.Api.Service.Commands;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarListApp.Api.Configuration.Endpoints;

internal static class AuthEndpointsExtension
{
    internal static void RegisterAuthEndpoints(this IEndpointRouteBuilder endpoints, IConfiguration configuration)
    {
        JwtSettings jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();

        endpoints.MapPost("auth/login", async (
            LoginDto loginDto, 
            UserManager<IdentityUser> _userManager) =>
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);

            if (user is null) return Results.Unauthorized();

            var isValidPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!isValidPassword) return Results.Unauthorized();

            // Generate Access Token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(user);
            var claims = await _userManager.GetClaimsAsync(user);
            var tokenClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("email_confirmed", user.EmailConfirmed.ToString())
            }
            .Union(claims)
            .Union(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var securityToken = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: tokenClaims,
                expires: DateTime.UtcNow.AddMinutes(jwtSettings.DurationInMinutes),
                signingCredentials: credentials
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);

            var response = new AuthResponseDto(user.Id, user.UserName, accessToken);

            return Results.Ok(response);
        }).AllowAnonymous();

        endpoints.MapPost("auth/register", async (
            IValidator<IdentityUserDto> validator, 
            IdentityUserDto identityUserDto, 
            UserManager<IdentityUser> _userManager) =>
        {
            var validationResult = await validator.ValidateAsync(identityUserDto);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            IdentityUser identityUser = new IdentityUser();
            identityUser.Email = identityUserDto.Email;
            identityUser.NormalizedEmail = identityUserDto.Email.ToUpperInvariant();
            identityUser.UserName = identityUserDto.UserName;
            identityUser.NormalizedUserName = identityUserDto.UserName.ToUpperInvariant();
            identityUser.EmailConfirmed = true;

            var result = await _userManager.CreateAsync(identityUser, identityUserDto.Password);
            if(!result.Succeeded)
            {
                ProblemDetails problemDetails = new ProblemDetails
                {
                    Title = $"Problem detected while registering a new user {identityUserDto.UserName}.",
                    Status = 406,
                    Detail = "Please find details about specific attempt failure."
                };

                foreach(var error in result.Errors)
                {
                    problemDetails.Extensions.Add(error.Code, error.Description);
                }

                return Results.Problem(problemDetails);
            }

            string validatedRole = identityUserDto.Role == "Administrator"
                ? "Administrator"
                : "User";

            await _userManager.AddToRoleAsync(identityUser, validatedRole);

            return Results.Created("URI to retrieve detail about the created user TODO", identityUser);
        }).AllowAnonymous();

        endpoints.MapPost("auth/forgot", async (
            string email,
            UserManager<IdentityUser> _userManager,
            IMediator _mediator,
            CancellationToken cancellationToken) =>
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null) return Results.NotFound();

            var token =  await _userManager.GeneratePasswordResetTokenAsync(user);

            var response = new PasswordForgotDto(email, token);

            SendEmailDto sendEmailDto = new SendEmailDto("villalbaweb@gmail.com", "villalbaweb@gmail.com", "SMPT Test", token);

            return await _mediator.Send(new SendEmailCommand(sendEmailDto), cancellationToken)
                ? Results.Ok(response)
                : Results.Ok();

        }).AllowAnonymous();

        endpoints.MapPost("auth/reset", async (
            PasswordResetDto passwordResetDto,
            UserManager<IdentityUser> _userManager) =>
        {
            var user = await _userManager.FindByEmailAsync(passwordResetDto.Email);

            if (user is null) return Results.NotFound();

            var reserPasswordResult = await _userManager.ResetPasswordAsync(user, passwordResetDto.Token, passwordResetDto.NewPassword);
            if (!reserPasswordResult.Succeeded)
            {
                    ProblemDetails problemDetails = new ProblemDetails
                    {
                        Title = $"Problem detected while reseting password for {passwordResetDto.Email}.",
                        Status = 406,
                        Detail = "Please find details about specific attempt failure."
                    };

                    foreach (var error in reserPasswordResult.Errors)
                    {
                        problemDetails.Extensions.Add(error.Code, error.Description);
                    }

                    return Results.Problem(problemDetails);
                }

            return Results.Ok("Password has been changed!");

        }).AllowAnonymous();

        endpoints.MapGet("auth/users", async(
            string role,
            UserManager<IdentityUser> _userManager,
            ClaimsPrincipal _user) =>
        {
            var user = await _userManager.GetUserAsync(_user);

            if(user is null) return Results.NotFound();

            var roles = await _userManager.GetRolesAsync(user);
            bool isAdmin = roles
                .ToList()
                .Exists(x => x.Equals("Administrator"));

            if (!isAdmin) return Results.Unauthorized();

            return Results.Ok(await _userManager.GetUsersInRoleAsync(role.ToString()));
        });
    }
}
