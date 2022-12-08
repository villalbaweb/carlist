using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowAll", a => a
        .AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod());
});

// local running
var dbPath = Path.Join(Directory.GetCurrentDirectory(), "carlist.db");
var conn = new SqliteConnection($"Data Source={dbPath}");

// azure deployment
//var conn = new SqliteConnection("Data Source=carlist.db");

builder.Services.AddDbContext<CarListDbContext>(o => o.UseSqlite(conn));

builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CarListDbContext>();

builder.Services.AddAuthentication(oprions =>
{
    oprions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    oprions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
    .RequireAuthenticatedUser()
    .Build();
});

var app = builder.Build();

//Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{

//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowAll");

app.MapGet("/cars", async (CarListDbContext db) => await db.Cars.ToListAsync());
app.MapGet("/cars/{id}", async (CarListDbContext db, int id) => await db.Cars.FindAsync(id) is Car car ? Results.Ok(car) : Results.NotFound());

app.MapPut("/cars/{id}", async (CarListDbContext db, int id, Car car) =>
{
    var record = await db.Cars.FindAsync(id);
    if (record is null) return Results.NotFound();

    record.Model = car.Model;
    record.Make = car.Make;
    record.Vin = car.Vin;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/cars/{id}", async (CarListDbContext db, int id) =>
{
    var record = await db.Cars.FindAsync(id);
    if (record is null) return Results.NotFound();

    db.Remove(record);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapPost("/cars", async (CarListDbContext db, Car car) =>
{
    await db.AddAsync(car);
    await db.SaveChangesAsync();
    
    return Results.Created($"/cars/{car.Id}", car);
});

app.MapPost("/login", async (LoginDto loginDto, CarListDbContext db, UserManager<IdentityUser> _userManager) => 
{
    var user = await _userManager.FindByNameAsync(loginDto.Username);

    if (user is null) return Results.Unauthorized();

    var isValidPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);

    if(!isValidPassword) return Results.Unauthorized();

    // Generate Access Token
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]));
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
        issuer: builder.Configuration["JwtSettings:Issuer"],
        audience: builder.Configuration["JwtSettings:Audience"],
        claims: tokenClaims,
        expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(builder.Configuration["JwtSettings:DurationInMinutes"])),
        signingCredentials: credentials
    );

    var accessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);

    var response = new AuthResponseDto(user.Id, user.UserName, accessToken);

    return Results.Ok(response);
}).AllowAnonymous();

app.Run();

internal record LoginDto(string Username, string Password);
internal record AuthResponseDto(string UserId, string Username, string Token);