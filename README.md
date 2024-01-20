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
* First, clone this repository, then change directory to the RandomBot folder
```
git clone https://github.com/nguyenhuy0905/random-bot.git
cd random-bot/RandomBot
```
- The RandomBot folder contains a file called ***init_script.sh*** to set up the *bot.config* folder.
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
version: '0.0.2'

services:
  mc-discord-bot:
    image: huy55465/random-bot-image:0.0.2
    volumes:
      - ./bot.config:/bot.config

```
- Run ``` docker compose up -d``` and enjoy!

- Alternatively, you can run the bot using the base code. But this can only be used for testing. In reality, this will take up your console, and you cannot start your Minecraft server.
```
dotnet run
```


### Changelog
- **0.0.2**: Started tweaking to fit a more "general" use case
- **0.0.1**: Release one for me and my own use case only