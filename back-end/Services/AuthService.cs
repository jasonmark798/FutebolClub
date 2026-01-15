using AlbumFigurinhas.Api.DTOs;
using AlbumFigurinhas.Api.Models;
using AlbumFigurinhas.Api.Services.Interfaces;
using System.Linq;

namespace AlbumFigurinhas.Api.Services;

public class AuthService : IAuthService
{
    private readonly TokenService _tokenService;

    private static readonly List<Usuario> _users = new List<Usuario>
    {
        new Usuario
        {
            Id = 1,
            Nome = "Admin Futebol",
            Email = "admin@futebol.com",
            Senha = "123"
        },
        new Usuario
        {
            Id = 2,
            Nome = "Paulo Lemos",
            Email = "paulo@futebol.com",
            Senha = "456"
        },
        new Usuario
        {
            Id = 3,
            Nome = "Convidado",
            Email = "convidado@futebol.com",
            Senha = "789"
        }
    };

    public AuthService(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public LoginResponseDto? Login(LoginDto loginDto)
    {
        var user = _users.FirstOrDefault(u => u.Email == loginDto.Email && u.Senha == loginDto.Senha);

        if (user != null)
        {
            var token = _tokenService.GenerateToken(user);
            return new LoginResponseDto
            {
                Token = token,
                Nome = user.Nome,
                Email = user.Email
            };
        }

        return null;
    }

    public LoginResponseDto? Register(RegisterDto registerDto)
    {
        if (_users.Any(u => u.Email == registerDto.Email))
        {
            return null; // Or throw an exception for already existing user
        }

        var newUser = new Usuario
        {
            Id = _users.Max(u => u.Id) + 1,
            Nome = registerDto.Nome,
            Email = registerDto.Email,
            Senha = registerDto.Senha
        };

        _users.Add(newUser);

        var token = _tokenService.GenerateToken(newUser);
        return new LoginResponseDto
        {
            Token = token,
            Nome = newUser.Nome,
            Email = newUser.Email
        };
    }
}
