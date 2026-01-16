using System.Text.Json;
using AlbumFigurinhas.Api.DTOs;
using AlbumFigurinhas.Api.Models;
using AlbumFigurinhas.Api.Services.Interfaces;

namespace AlbumFigurinhas.Api.Services;

public class TimeService : ITimeService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly int[] _timesIds = { 118, 119, 120, 121, 124, 126, 127, 
                                         128, 130, 131, 132, 133, 134, 135,
                                         136, 147, 794, 1062, 1198, 7848 }; // Todos os clubes da Serie A do Campeonato Brasileiro 2026

    public TimeService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public Task<IEnumerable<Time>> ObterTimes()
    {
        return ObterTimesAsync();
    }

    public async Task<IEnumerable<Time>> ObterTimesAsync()
    {
        var client = _httpClientFactory.CreateClient("ApiFootball");
        var times = new List<Time>();

        foreach (var timeId in _timesIds)
        {
            var response = await client.GetAsync($"/teams?id={timeId}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonSerializer.Deserialize<ApiResponse<ItemTime>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
                
            });
            Console.WriteLine($"TimeId: {timeId} | Response.Count: {apiResponse?.Response.Count}");
            if (apiResponse != null && apiResponse.Response.Any())
            {
                var teamDto = apiResponse.Response.First().Team;
                times.Add(new Time
                {
                    Id = teamDto?.Id ?? 0,
                    Nome = teamDto?.Name ?? string.Empty,
                    Escudo = teamDto?.Logo ?? string.Empty,
                    AnoFundacao = teamDto?.Founded ?? 0
                });
            } else
            {
                continue;
            }
        }

        return times;
    }
}
