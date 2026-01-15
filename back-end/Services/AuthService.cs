using AlbumFigurinhas.Api.DTOs;
using AlbumFigurinhas.Api.Models;
using AlbumFigurinhas.Api.Services.Interfaces;

namespace AlbumFigurinhas.Api.Services;

public class AuthService : IAuthService
{
    private readonly TokenService _tokenService;

    // Hardcoded user for demonstration
    private readonly Usuario _adminUser = new Usuario
    {
        Id = 1,
        Nome = "Admin Futebol",
        Email = "admin@futebol.com",
        Senha = "123" // Plain text for demo only
    };

    public AuthService(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public LoginResponseDto Login(LoginDto loginDto)
    {
        // Simple check
        if (loginDto.Email == _adminUser.Email && loginDto.Senha == _adminUser.Senha)
        {
            var token = _tokenService.GenerateToken(_adminUser);
            return new LoginResponseDto
            {
                Token = token,
                Nome = _adminUser.Nome,
                Email = _adminUser.Email
            };
        }

        return null; // Invalid login
    }
}
