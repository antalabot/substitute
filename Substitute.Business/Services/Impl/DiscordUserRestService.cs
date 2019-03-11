using Discord;
using Discord.Rest;
using Substitute.Business.DataStructs.Guild;
using Substitute.Domain.Data;
using Substitute.Domain.DataStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Substitute.Business.Services.Impl
{
    public class DiscordUserRestService : DiscordRestServiceBase, IDiscordUserRestService
    {
        #region Private constants
        private const string CLASS_NAME = "DiscordUserRestService";
        #endregion

        #region Private readonly variables
        private readonly ICache _cache;
        private readonly string _token;

        private readonly TimeSpan _getGuildsExpiration = new TimeSpan(0, 15, 0);
        #endregion

        #region Constructor
        public DiscordUserRestService(ICache cache, string token)
            : base(TokenType.Bearer, token)
        {
            _cache = cache;
            _token = token;
        }
        #endregion

        #region Implementation of IDiscordUserRestService
        public async Task<IEnumerable<UserGuild>> GetGuilds()
        {
            return await _cache.GetOrCreateAsync($"{CLASS_NAME}|GetGuilds|{_token}", FetchGuilds);
        }
        #endregion

        #region Private helpers
        private async Task<IEnumerable<UserGuild>> FetchGuilds(ICacheEntity entity)
        {
            entity.SlidingExpiration = _getGuildsExpiration;
            IEnumerable<RestUserGuild> list = await _discordClient.GetGuildSummariesAsync().FlattenAsync();
            return list.Select(g => new UserGuild
            {
                CanManage = g.Permissions.ManageGuild,
                IconUrl = g.IconUrl,
                Id = g.Id,
                IsAdministrator = g.Permissions.Administrator,
                IsOwner = g.IsOwner,
                Name = g.Name
            });
        }
        #endregion
    }
}
