using AlbumFigurinhas.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AlbumFigurinhas.Api.Endpoints;

public static class TimeEndpoints
{
    public static void MapTimeEndpoints(this WebApplication app)
    {
        app.MapGet("/times", async (ITimeService timeService) =>
        {
            var times = await timeService.ObterTimes();
            return times.Any() ? Results.Ok(times) : Results.NotFound("Nenhum time encontrado. Verifique a API Key.");
        })
        .WithName("ObterTimes");
    }
}
