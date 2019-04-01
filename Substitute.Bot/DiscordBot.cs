using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Substitute.Bot.Models;
using Substitute.Domain.Data.Entities;
using Substitute.Domain.DataStore;
using Substitute.Domain.Enums;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Substitute.Bot
{
    public class DiscordBot : IDiscordBot
    {
        private const string DISCORD_TOKEN_KEY = "Discord:Token";

        private readonly IStorage _storage;
        private readonly ISingletonContext _context;
        private readonly IConfiguration _configuration;
        private readonly DiscordSocketClient _client;

        public DiscordBot(IStorage storage, ISingletonContext context, IConfiguration configuration)
        {
            _storage = storage;
            _context = context;
            _configuration = configuration;
            _client = new DiscordSocketClient();

            Configure();
        }

        public async Task LoginAndStart()
        {
            await _client.LoginAsync(TokenType.Bot, _configuration[DISCORD_TOKEN_KEY]);
            await _client.StartAsync();
            await _client.SetActivityAsync(new BotActivity { Name = "Bits and Bytes", Type = ActivityType.Playing });
        }

        private void Configure()
        {
            _client.MessageReceived += MessageReceived;
        }

        private async Task MessageReceived(SocketMessage message)
        {
            string command = (message.Content ?? "").Trim();
            if (!message.Author.IsBot && !string.IsNullOrWhiteSpace(command) && command.StartsWith('>'))
            {
                command = command.Substring(1).ToLower();
                SocketGuild guild = _client.Guilds.Single(g => g.Channels.Any(c => c.Id == message.Channel.Id));
                ImageResponse image = _context.Get<ImageResponse>().SingleOrDefault(i => i.Command == command && i.GuildId == guild.Id);
                if (image != null)
                {
                    using (message.Channel.EnterTypingState())
                    {
                        byte[] data = await _storage.Get(EStorage.ImageResponse, image.Fielename, image.GuildId.GetValueOrDefault().ToString());
                        using (MemoryStream stream = new MemoryStream(data))
                        {
                            await message.Channel.SendFileAsync(stream, image.Fielename);
                        }
                    }
                }
            }
        }
    }
}
