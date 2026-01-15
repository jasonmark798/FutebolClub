using AlbumFigurinhas.Api.Models;

namespace AlbumFigurinhas.Api.Services.Interfaces;

public interface IJogadorService
{
    Task<IEnumerable<Jogador>> ObterJogadoresPorTimeId(int timeId);
}