using AlbumFigurinhas.Api.Services.Interfaces;

namespace AlbumFigurinhas.Api.Endpoints;

public static class JogadoresEndpoints
{
    public static void MapJogadoresEndpoints(this WebApplication app)
    {
        app.MapGet("/api/jogadores/{timeId:int}", async (int timeId, IJogadorService jogadorService) =>
        {
            var jogadores = await jogadorService.ObterJogadoresPorTimeId(timeId);
            return Results.Ok(jogadores);
        })
        .WithName("GetJogadoresPorTimeId")
        .WithTags("Jogadores");
    }
}