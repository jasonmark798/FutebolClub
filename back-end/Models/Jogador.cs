namespace AlbumFigurinhas.Api.Models;

public class Jogador
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Posicao { get; set; } = string.Empty;
    public int TimeId { get; set; }
}