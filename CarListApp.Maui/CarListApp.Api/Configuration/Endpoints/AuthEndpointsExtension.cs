using CarListApp.Api.Core.Dto;
using CarListApp.Api.Core.Settings;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarListApp.Api.Configuration.Endpoints;

internal static class AuthEndpointsExtension
{
    internal static void RegisterAuthEndpoints(this IEndpointRouteBuilder endpoints, JwtSettings jwtSettings)
    {
        endpoints.MapPost("/login", async (
            LoginDto loginDto, 
            CarListDbContext db, 
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

        endpoints.MapPost("/register", async (
            IValidator<IdentityUserDto> validator, 
            IdentityUserDto identityUserDto, 
            CarListDbContext db, 
            UserManager<IdentityUser> _userManager) =>
        {
            var validationResult = await validator.ValidateAsync(identityUserDto);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            // https://code-maze.com/user-registration-aspnet-core-identity/

            return Results.Ok();
        }).AllowAnonymous();
    }
}
