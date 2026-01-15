using AlbumFigurinhas.Api.Services;
using AlbumFigurinhas.Api.Services.Interfaces;
using AlbumFigurinhas.Api.Endpoints;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// HTTP Client for API Football
builder.Services.AddHttpClient("ApiFootball", client =>
{
    var config = builder.Configuration.GetSection("ApiFootball");
    client.BaseAddress = new Uri(config["BaseUrl"] ?? "https://v3.football.api-sports.io");
    client.DefaultRequestHeaders.Add("x-apisports-key", config["ApiKey"]);
});

// Domain Services
builder.Services.AddScoped<IJogadorService, JogadorService>();
builder.Services.AddScoped<ITimeService, TimeService>();

// Auth Services
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// JWT Authentication Configuration
var jwtKey = builder.Configuration["Jwt:Key"] ?? "chave_super_secreta_padrao_para_desenvolvimento_123";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "FutebolClubApi";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "FutebolClubApp";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization(); // Required for UseAuthorization middleware

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173", "http://127.0.0.1:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactApp");

// Auth Middleware must be before Endpoints
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

// Map Endpoints
app.MapJogadoresEndpoints();
app.MapTimeEndpoints();
app.MapAuthEndpoints();

app.Run();