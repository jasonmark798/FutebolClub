using System.Text.Json.Serialization;

namespace AlbumFigurinhas.Api.DTOs;

public class ApiResponse<T>
{
    [JsonPropertyName("response")]
    public List<T> Response { get; set; } = new();
}

public class ItemTime
{
    [JsonPropertyName("team")]
    public TeamInfo? Team { get; set; }
}

public class TeamInfo
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("logo")]
    public string Logo { get; set; } = string.Empty;

    [JsonPropertyName("founded")]
    public int? Founded { get; set; }
}


public class ItemSquad
{
    [JsonPropertyName("players")]
    public List<PlayerInfo> Players { get; set; } = new();
}

public class PlayerInfo
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("age")]
    public int? Age { get; set; }

    [JsonPropertyName("number")]
    public int? Number { get; set; }

    [JsonPropertyName("position")]
    public string Position { get; set; } = string.Empty;

    [JsonPropertyName("photo")]
    public string Photo { get; set; } = string.Empty;
}
