using AlbumFigurinhas.Api.Models;

namespace AlbumFigurinhas.Api.Services.Interfaces;

public interface ITimeService
{
    Task<IEnumerable<Time>> ObterTimes();
}
