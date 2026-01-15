using AlbumFigurinhas.Api.DTOs;

namespace AlbumFigurinhas.Api.Services.Interfaces;

public interface IAuthService
{
    LoginResponseDto Login(LoginDto loginDto);
}
