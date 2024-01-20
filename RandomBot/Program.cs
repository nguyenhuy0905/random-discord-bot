using System.Text.Json.Nodes;
using Discord;
using Discord.WebSocket;

namespace RandomBot;

public class Program{
    private DiscordSocketClient _client;

    /// <summary>
    /// The bot token. Access this through your Discord developer portal
    /// </summary>
    private static string? token;

    /// <summary>
    /// Your Ngrok tunnel URL. It should be in this format: http://<your-server-ip>:<ngrok-port>/api/tunnels
    /// </summary>
    private static string? url;

    /// <summary>
    /// The Discord server ID you want the bot to access.
    /// </summary>
    private static ulong guild_id;

    /// <summary>
    /// The text channel ID of the channel, in the guild specified above, you want the bot to send and receive notifications from
    /// </summary>
    private static ulong text_channel_id;

    public Program()
    {
        _client = new DiscordSocketClient(); 

        // configuring the bot
        BotConfig? cfg = new();
        BotConfig.Deserialize(out cfg);
        // if the user has yet to configure the bot (aka, no ./bot.config/bot.config.json written, or the file was modified)
        if(cfg == null) throw new NullReferenceException("Error, no config is written yet");

        // assigning variables
        token = cfg.Token!;
        url = cfg.URL!;
        guild_id = cfg.ChatGuildID!;
        text_channel_id = cfg.TextChannelID!;
        
    }

    public static Task Main(string[] args) => new Program().MainAsync();

    private async Task MainAsync(){

        
        _client.Log += Log;
        // fires the server address once the bot is ready to work
        _client.Ready += () => {
            _client.GetGuild(guild_id).GetTextChannel(text_channel_id).SendMessageAsync($"### The Minecraft server address is \n ```\n{GetPublicUrl().Result}\n```");
            return Task.CompletedTask;
        };
        // login and start the bot
        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

    }

    private Task Log(LogMessage msg){
        Console.WriteLine(msg);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Returns the Minecraft server's Ngrok URL
    /// </summary>
    /// <returns></returns>
    private static async Task<string> GetPublicUrl(){
        using HttpClient http = new HttpClient();
        string json;
        try
        {
            json = await http.GetStringAsync(url);
        }
        catch (System.Exception)
        {
            return "None";
        }

        JsonNode mainNode = JsonNode.Parse(json)!;
        return mainNode["tunnels"]![0]!["public_url"]!.ToString()[6..];
    }
}