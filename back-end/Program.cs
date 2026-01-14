using AlbumFigurinhas.Api.Services;
using AlbumFigurinhas.Api.Services.Interfaces;
using AlbumFigurinhas.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient("ApiFootball", client =>
{
    var config = builder.Configuration.GetSection("ApiFootball");
    client.BaseAddress = new Uri(config["BaseUrl"] ?? "https://v3.football.api-sports.io");
    client.DefaultRequestHeaders.Add("x-apisports-key", config["ApiKey"]);
});

builder.Services.AddScoped<IJogadorService, JogadorService>();
builder.Services.AddScoped<ITimeService, TimeService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapJogadoresEndpoints();
app.MapTimeEndpoints();

app.Run();