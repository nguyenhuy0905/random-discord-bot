#!/bin/bash

# accepts 3 arguments

echo "Please write your bot's token";

read token;

echo "Please write your guild ID";

read guild_id;

echo "Please write your text channel ID";

read text_channel_id;

echo "Please write your Ngrok tunnels URL (format: http://<your-host-ip-address>:<port>/api/tunnels):"

read url;

echo "Writing your info into ./bot.config.json";

cat > ./bot.config.json <<EDL
{
    "token": "${token}",
    "guild_id": ${guild_id},
    "text_channel_id": ${text_channel_id},
    "url": "${url}"
}
EDL

echo "Finished writing!"
