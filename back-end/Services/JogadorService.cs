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
                    });
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao buscar jogadores do time {timeId}: {ex.Message}");
        }

        return GetMockJogadores(timeId);
    }

    private IEnumerable<Jogador> GetMockJogadores(int timeId)
    {
        Console.WriteLine($"API Limit reached or no data. Loading fallback mock players for team {timeId}...");
        var players = new List<Jogador>();

        if (timeId == 126) // SPFC
        {
            players.Add(new Jogador { Id = 1, Nome = "Lucas Moura", Posicao = "Atacante", TimeId = timeId });
            players.Add(new Jogador { Id = 2, Nome = "Calleri", Posicao = "Atacante", TimeId = timeId });
            players.Add(new Jogador { Id = 3, Nome = "Luciano", Posicao = "Meio-Campo", TimeId = timeId });
        }
        else if (timeId == 127) // Flamengo
        {
            players.Add(new Jogador { Id = 4, Nome = "Gabigol", Posicao = "Atacante", TimeId = timeId });
            players.Add(new Jogador { Id = 5, Nome = "Arrascaeta", Posicao = "Meio-Campo", TimeId = timeId });
            players.Add(new Jogador { Id = 6, Nome = "Pedro", Posicao = "Atacante", TimeId = timeId });
        }
        else if (timeId == 131) // Corinthians
        {
            players.Add(new Jogador { Id = 7, Nome = "Yuri Alberto", Posicao = "Atacante", TimeId = timeId });
            players.Add(new Jogador { Id = 8, Nome = "Garro", Posicao = "Meio-Campo", TimeId = timeId });
            players.Add(new Jogador { Id = 9, Nome = "Memphis Depay", Posicao = "Atacante", TimeId = timeId });
        }
        else
        {
            players.Add(new Jogador { Id = 10, Nome = "Jogador Exemplo 1", Posicao = "Meio-Campo", TimeId = timeId });
            players.Add(new Jogador { Id = 11, Nome = "Jogador Exemplo 2", Posicao = "Defender", TimeId = timeId });
        }

        return players;
    }
}