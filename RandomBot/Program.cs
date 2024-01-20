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
        BotConfig.Deserialize(out BotConfig? cfg);
        // if the user has yet to configure the bot (aka, no ./bot.config/bot.config.json written, or the file was modified)
        if (cfg == null) throw new NullReferenceException("Error, no config is written yet");

        // assigning variables
        token = cfg.Token!;
        url = cfg.URL!;
        guild_id = cfg.ChatGuildID!;
        text_channel_id = cfg.TextChannelID!;
        
    }

    /*
    Why are there 2 Mains here?
    Main is accessed by the program;
    MainAsync is where the actual logic runs.
    Most, if not all, execution of the Discord.Net library wants to be asynchronous (which, I guess is to not make the bot unresponsive when it is running a task)
    */
    public static Task Main() => new Program().MainAsync();

    private async Task MainAsync(){
        _client.Log += Log;
        // fires the server address once the bot is ready to work
        _client.Ready += () => {
            // You can change this message from Minecraft server to whatever game server you are currently hosting
            _client.GetGuild(guild_id).
                GetTextChannel(text_channel_id).
                SendMessageAsync($"### The Minecraft server address is \n ```\n{GetPublicUrl().Result}\n```");
            return Task.CompletedTask;
        };
        // login and start the bot
        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

    }

    /// <summary>
    /// Basically checks how the bot's doing, if it throws any errors...
    /// </summary>
    private Task Log(LogMessage msg){
        Console.WriteLine(msg);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Returns the Minecraft server's Ngrok URL
    /// </summary>
    private static async Task<string> GetPublicUrl(){
        // get the JSON string from the NGROK URL
        using HttpClient http = new();
        string json;
        try
        {
            json = await http.GetStringAsync(url);
        }
        catch (Exception)
        {
            return "None";
        }

        /*
        NOTE: THIS CODE ASSUMES YOUR TCP TUNNEL IS RUNNING AS THE FIRST NGROK TUNNEL
        If not, please go to your NGROK URL and check to see what tunnel you want your server to connect to, then modify this code
        EG, the tunnel you want to share is the 2nd one, change the return line to
            return mainNode["tunnels"]![1]!["public_url"]!.ToString()[6..];
        */
        JsonNode mainNode = JsonNode.Parse(json)!;
        return mainNode["tunnels"]![0]!["public_url"]!.ToString()[6..];
    }
}