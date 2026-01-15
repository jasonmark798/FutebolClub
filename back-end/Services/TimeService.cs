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
                    Console.WriteLine($"API Response for team {id}: {content}");

                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var apiResponse = JsonSerializer.Deserialize<ApiResponse<ItemTime>>(content, options);

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
        var timesList = results.Where(t => t != null).Cast<Time>().ToList();

        if (!timesList.Any())
        {
            Console.WriteLine("API Limit reached or no data. Loading fallback mock teams...");
            return new List<Time>
            {
                new Time { Id = 126, Nome = "Sao Paulo", Escudo = "https://media.api-sports.io/football/teams/126.png", AnoFundacao = 1930 },
                new Time { Id = 127, Nome = "Flamengo", Escudo = "https://media.api-sports.io/football/teams/127.png", AnoFundacao = 1895 },
                new Time { Id = 131, Nome = "Corinthians", Escudo = "https://media.api-sports.io/football/teams/131.png", AnoFundacao = 1910 },
                new Time { Id = 119, Nome = "Internacional", Escudo = "https://media.api-sports.io/football/teams/119.png", AnoFundacao = 1909 },
                new Time { Id = 130, Nome = "Gremio", Escudo = "https://media.api-sports.io/football/teams/130.png", AnoFundacao = 1903 }
            };
        }

        return timesList;
    }
}
