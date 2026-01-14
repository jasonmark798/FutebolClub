using System.Text.Json;
using AlbumFigurinhas.Api.DTOs;
using AlbumFigurinhas.Api.Models;
using AlbumFigurinhas.Api.Services.Interfaces;

namespace AlbumFigurinhas.Api.Services;

public class JogadorService : IJogadorService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public JogadorService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<Jogador>> ObterJogadoresPorTimeId(int timeId)
    {
        var client = _httpClientFactory.CreateClient("ApiFootball");
        try
        {
            var response = await client.GetAsync($"players/squad?team={timeId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<ItemSquad>>(content);
                var squad = apiResponse?.Response?.FirstOrDefault()?.Players;

                if (squad != null)
                {
                    return squad.Select(p => new Jogador
                    {
                        Id = p.Id,
                        Nome = p.Name,
                        Posicao = p.Position,
                        TimeId = timeId,
                        // Adicionando propriedade de foto se existir no modelo, 
                        // mas o modelo atual Jogador.cs só tem Id, Nome, Posicao, TimeId.
                        // Podemos expandir o modelo depois.
                    });
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao buscar jogadores do time {timeId}: {ex.Message}");
        }

        return Enumerable.Empty<Jogador>();
    }
}