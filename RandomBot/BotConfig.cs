using System.Text.Json;
using System.Text.Json.Serialization;

namespace RandomBot;

/// <summary>
/// Contains 3 configs, <code>BotToken</code> <code>ChatGuildID</code> and <code>TextChannelID</code>
/// </summary>
public class BotConfig{
    [JsonPropertyName("token")]
    public string? Token {get; set;}

    [JsonPropertyName("guild_id")]
    public ulong ChatGuildID {get; set;}

    [JsonPropertyName("text_channel_id")]
    public ulong TextChannelID {get; set;}

    [JsonPropertyName("url")]
    public string? URL {get; set;}

    public static void Deserialize(out BotConfig? config){
        using StreamReader reader = new(@"./bot-config/bot.config.json");
        string json = reader.ReadToEnd();
        config = JsonSerializer.Deserialize<BotConfig>(json);
        Console.WriteLine("Finished configuring bot");
    }
}