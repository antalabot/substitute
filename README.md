# Substitute Discord Bot

**WARNING! This piece of software is far from being production-ready!**

This is more of a sandbox than a real app.

## What is Substitute?

*Substitute* is a Discord bot manageable through website.

*Substitute* serves two purposes:
- It is complementary to my Discord's [RED Discord Bot](https://github.com/Cog-Creators/Red-DiscordBot)
- Gives me chance to learn [Discord.NET](https://github.com/discord-net/Discord.Net) and .NET Core

Current *Substitute*'s abilities:
- Manage guild roles' access levels
- Manage images that bot uses as responses to `>` commands in channels

## How to configure it?

Substitute uses [build-in configuration providers](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-2.2) and it's up to developer which method they use.

Configuration keys necessary for *Substitute* are:
- `ConnectionStrings:SubstituteDatabase` - PostgreSQL connection string
- `Discord:Id` - Discord app client id
- `Discord:Secret` - Discord app client secret
- `Discord:Token` - Discord app bot token

Sample config used as `appsettings.Development.json`:
```json
{
    "ConnectionStrings": {
        "SubstituteDatabase": "Host=host;Port=port;Database=database;Username=username;Password=password"
    },
    "Discord": {
        "Id": "##################",
        "Secret": "********************************",
        "Token": "***********************************************************"
    }
}
```

## How to build it?

You need to install [.NET Core SDK 2.2](https://dotnet.microsoft.com/download/dotnet-core/2.2) and any git client. Steps (put those commands in your command line):
1. Clone the repository: `git clone https://github.com/antalabot/substitute.git`
2. Build it using `dotnet build`

## How to run and use it?

After configuring and building *Substitute*:
1. (one time) Create database and [apply migrations](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/) by executing `dotnet ef database update` in your command line
2. Execute `dotnet run --project Substitute.Webpage` command in your command line
3. Open address you see in console in your browser
4. Log in using Discord
5. (one time) go to `/Home/Setup` and claim ownership
6. Choose server (for bot to join in, if it's not already a member of said guild)

## License

Project available under [Apache License 2.0](LICENSE).
