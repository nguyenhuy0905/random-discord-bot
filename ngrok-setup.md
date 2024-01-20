# HOW TO: set up a NGROK proxy for your Minecraft server
> This will guide you through how to create a NGROK proxy to bypass your ISP's CG-NAT. I prefer YouTube guides like [this one](https://www.youtube.com/watch?v=SZmc5uoNCko&t=1249s) to a document like this. If you can (and I'm pretty sure you can), go watch that video instead. I got the idea for this from a YouTube guide, y'know
>> DISCLAIMER: THIS IS INTENDED FOR LINUX.
### Why would I want to do this?
- Sometimes, the ISP implements this thing called CG-NAT. 
- Basically, you may have your Minecraft server set up and accessible inside your personal network, but your friends outside cannot.
- A proxy like Ngrok helps bypassing CG-NAT.
- Technically, you can pay subscription to Ngrok or Cloudflared (or, who else, there are probably more of them) to get a static TCP proxy. But, I don't think hosting a Minecraft server for your friends to play is worth that cost.
### CAUTION
- For a free ngrok account, if anyone gets your IP address, they can access http://[your-ip]:[ngrok-port]/api/tunnels and see your server address.
### Set up
> This is taken from the ngrok documentation page. [Check their documentation for a more detailed guide](https://ngrok.com/docs/getting-started/?os=linux)
- Create a ngrok account, if you haven't, then copy the auth token. It should be on this command line:
```
ngrok config add-authtoken [your-token]
```
- Install ngrok
```
curl -s https://ngrok-agent.s3.amazonaws.com/ngrok.asc | \
  sudo tee /etc/apt/trusted.gpg.d/ngrok.asc >/dev/null && \
  echo "deb https://ngrok-agent.s3.amazonaws.com buster main" | \
  sudo tee /etc/apt/sources.list.d/ngrok.list && \
  sudo apt update && sudo apt install ngrok
```
- Add authentication to the default ngrok.yml
```
ngrok config add-authtoken 2b1app3zCqxBRbvWZdHYQQqQ0KA_3qfPVfdjDbWCo6Uxxinxd
```
- Open the port. Let's say you are using the default server config
```
ngrok tcp 25565
```
- You will then receive your TCP in this form:
```
tcp://[ngrok-io-address]:[port]
```
- Copy the [ngrok-io-address]:[port] and start your Minecraft server. Try to get into the server using the address you just copied to confirm that everything is working.
- Well, but, you can see this process also occupies our console. That's not good since you need the console to start the Minecraft server.

### Set up ngrok as a systemd service
> This is copied from the YouTube guide linked above
- [Click here to head to the documentation of systemd-ngrok](https://github.com/vincenthsu/systemd-ngrok).
- Clone ```systemd-ngrok``` then change directory:
```
https://github.com/vincenthsu/systemd-ngrok.git
cd systemd-ngrok
```
- Configure ngrok.yml
```
nano ngrok.yml
```
- The file should look like this:
```
version: "2"
authtoken: <add_your_token_here>
tunnels:
    web:
        proto: http
        addr: 80
    ssh:
        proto: tcp
        addr: 22
```
- Delete the file's content and copy this in:
```
version: "2"
authtoken: [your-token-here]
region: [your-timezone-here]
web-addr: [your-server-ip-here]:[choose-a-port]
tunnels:
    mc:
        addr: 25565
        proto: tcp

```
- Configure your tokem, timezone, and server IP. For port, I would say 4040. But I think you can choose.
- Run the install script
```
chmod +x ./install.sh
sudo ./install.sh [your-token]
```
- After the installation, go to http://[your-server-ip]:[the-port-you-chose]/api/tunnels


