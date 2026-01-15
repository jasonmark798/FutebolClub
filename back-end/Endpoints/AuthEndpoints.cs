using AlbumFigurinhas.Api.DTOs;
using AlbumFigurinhas.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AlbumFigurinhas.Api.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        app.MapPost("/login", (LoginDto loginDto, IAuthService authService) =>
        {
            var result = authService.Login(loginDto);
            return result != null ? Results.Ok(result) : Results.Unauthorized();
        })
        .WithName("Login")
        .WithTags("Auth");
    }
}
