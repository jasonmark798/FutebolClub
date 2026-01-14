using System.Text.Json;
using AlbumFigurinhas.Api.DTOs;
using AlbumFigurinhas.Api.Models;
using AlbumFigurinhas.Api.Services.Interfaces;

namespace AlbumFigurinhas.Api.Services;

public class TimeService : ITimeService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly int[] _timesIds = { 126, 127, 131, 119, 130 }; // SPFC, Flamengo, Corinthians, Inter, Gremio

    public TimeService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<Time>> ObterTimes()
    {
        var client = _httpClientFactory.CreateClient("ApiFootball");
        var times = new List<Time>();

        // Paralelize requests for better performance
        var tasks = _timesIds.Select(async id =>
        {
            try
            {
                var response = await client.GetAsync($"teams?id={id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonSerializer.Deserialize<ApiResponse<ItemTime>>(content);

                    var teamData = apiResponse?.Response?.FirstOrDefault()?.Team;
                    if (teamData != null)
                    {
                        return new Time
                        {
                            Id = teamData.Id,
                            Nome = teamData.Name,
                            Escudo = teamData.Logo,
                            AnoFundacao = teamData.Founded ?? 0
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                // Simple error handling: ignore failed requests or log them
                Console.WriteLine($"Erro ao buscar time {id}: {ex.Message}");
            }
            return null;
        });

        var results = await Task.WhenAll(tasks);
        return results.Where(t => t != null).Cast<Time>();
    }
}
