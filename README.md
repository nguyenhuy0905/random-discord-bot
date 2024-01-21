# random-bot
> My utility Discord bot to update NGROK server change

### Initial ranting: why I made this
- I have a Minecraft server hosted on my computer. I don't want to pay Cloudflare or Ngrok just to have a static TCP address.
- The Discord bot to update the server was the idea of Hardware Haven. [Check his guide on exactly this topic here](https://www.youtube.com/watch?v=SZmc5uoNCko&t=1249s)
- I do this mainly to, learn something. I don't know if this counts as plagialism
### Requirements
- You have created a Discord bot that has the permission to send message.
- You have Docker installed. Optionally, .NET SDK 7.0
- Of course, you have set up your Ngrok proxy. [Click here if you haven't](/ngrok-setup.md).
### How do I use this?
### Little to no configuration at all
> NOTE: if you fall into any of these cases: your Ngrok tunnel you want to share is not the first tunnel, or you don't want the bot to say your server is a Minecraft server, head to [here](#if-you-want-some-extra-configurations).
- Create some folders:
```
touch docker-compose.yml
mkdir bot-config
nano docker-compose.yml
```
- Add these into the docker-compose file:
```
services:
  mc-discord-bot:
    image: huy55465/random-discord-bot:0.1.1
    volumes:
      - ./bot-config:/RandomBot/bot-config
      - ./bot-config/bot.config.json:/RandomBot/bot.config.json
```
- Save the file then run this command
```
nano ./bot-config/bot.config.json
```
- Then paste the following into the file:
```
{
  "token": [your-bot-token],
  "guild_id": [your-guild-id],
  "text_channel_id": [your-text-channel-id],
  "url": [your-ngrok-tunnel-url]
}
```
- Your bot token can be obtained into your Discord Developer page. The NGROK tunnel URL is in this format: tcp://[server-ip]:[ngrok-port]/api/tunnels
- Once you saved the file, run this command:
```
docker compose up -d
```
- If everything works out correctly, your Discord bot should send a message about the Minecraft server address in the appropriate chat channel.

### If you want some extra configurations
- First, clone this repository, then change directory to the RandomBot folder
```
git clone https://github.com/nguyenhuy0905/random-discord-bot.git
cd random-discord-bot/RandomBot
```
- The RandomBot folder contains a file called ***init_script.sh*** to set up the *bot.config* folder.
- Create the bot-config folder:
```
mkdir bot-config
```
- Run the config by:
```
chmod +x ./init_script.sh
sudo ./init_script.sh
```

### Initial configuration
- The script will prompt 4 questions: the bot's token, the guild ID, the text channel ID and the URL to your host's NGROK tunnel(s).
- (NOTE: this assumes you only have 1 tunnel open. To check the number of tunnels, head to [http://[your-host-ip]:[ngrok-port]/api/tunnels]())
- A guide for more-than-1-port will be written in some time. Or maybe not. I am lazy.

### Get the bot running
> NOTE: Currently, I haven't made the docker image public, so the method using ``` docker compose up -d``` does NOT work.
- The base docker-compose.yml should be good to go:
```
version: '0.1.1'

services:
  mc-discord-bot:
    image: huy55465/random-discord-bot:0.1.1
    volumes:
      - ./bot.config:/bot.config

```
- Run ``` docker compose up -d``` and enjoy!

- Alternatively, you can run the bot using the base code. But this can only be used for testing. In reality, this will take up your console, and you cannot start your Minecraft server.
```
dotnet run
```


### Changelog
- **0.1.1**: Ready for more "public use". I omitted 0.1.0 because that version didn't even start
- **0.0.2**: Started tweaking to fit a more "general" use case
- **0.0.1**: Release one for me and my own use case only

### What's next for this project?
- Maybe just simple as change the mount location. 
- Add some command utilities, i.e. what version is the server on, what mods the server requires.