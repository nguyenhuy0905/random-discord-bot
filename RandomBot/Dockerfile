FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /RandomBot

# copy absolutely everything inside the RandomBot folder
COPY . ./
RUN dotnet restore
# Build and publish
RUN dotnet publish -c Release -o out

# Runtime
FROM mcr.microsoft.com/dotnet/runtime:7.0
WORKDIR /RandomBot
COPY --from=build-env /RandomBot/out .
ENTRYPOINT ["dotnet", "RandomBot.dll"]
