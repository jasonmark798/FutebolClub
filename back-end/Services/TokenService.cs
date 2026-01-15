using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AlbumFigurinhas.Api.Models;
using Microsoft.IdentityModel.Tokens;

namespace AlbumFigurinhas.Api.Services;

public class TokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(Usuario usuario)
    {
        var keyString = _configuration["Jwt:Key"] ?? "chave_super_secreta_padrao_para_desenvolvimento_123";
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("id", usuario.Id.ToString()),
            new Claim("nome", usuario.Nome)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"] ?? "FutebolClubApi",
            audience: _configuration["Jwt:Audience"] ?? "FutebolClubApp",
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
