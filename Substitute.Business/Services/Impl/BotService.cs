using Microsoft.Extensions.Configuration;
using Substitute.Business.DataStructs.Guild;
using Substitute.Domain;
using Substitute.Domain.DataStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Substitute.Business.Services.Impl
{
    public class BotService : IBotService
    {
        private const string DISCORD_ID_KEY = "Discord:Id";
        private const string CACHE_KEY = "BOT";
        private const ulong PERMISSIONS = 8;

        private readonly ICache _cache;
        private readonly IDiscordBotRestService _discordBotService;
        private readonly IConfiguration _configuration;

        public BotService(ICache cache, IDiscordBotRestService discordBotService, IConfiguration configuration)
        {
            _cache = cache;
            _discordBotService = discordBotService;
            _configuration = configuration;
        }

        public string GetJoinLink(ulong guildId, string returnUrl)
        {
            return $"https://discordapp.com/oauth2/authorize?client_id={_configuration[DISCORD_ID_KEY]}&scope=bot&permissions={PERMISSIONS}&redirect_url={returnUrl}";
        }

        public async Task<bool> HasJoined(ulong guildId)
        {
            IEnumerable<GuildModel> list = await _cache.GetOrCreateAsync($"{CACHE_KEY}|Guilds", async entity =>
            {
                entity.SlidingExpiration = TimeSpan.FromMinutes(1);
                return await _discordBotService.GetGuilds();
            });
            return list.Any(g => g.Id == guildId);
        }
    }
}
