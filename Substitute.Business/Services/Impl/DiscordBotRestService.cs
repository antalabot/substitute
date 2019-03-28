using Discord;
using Discord.Rest;
using Substitute.Business.DataStructs.Guild;
using Substitute.Domain;
using Substitute.Domain.DataStore;
using Substitute.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Substitute.Business.Services.Impl
{
    public class DiscordBotRestService : DiscordRestServiceBase, IDiscordBotRestService
    {
        #region Private constants
        private const string CLASS_NAME = "DiscordBotRestService";
        #endregion
        
        #region Constructor and destructor
        public DiscordBotRestService()
            : base(TokenType.Bot, Settings.DiscordToken)
        {
        }
        #endregion

        #region Public methods
        public async Task<IEnumerable<RoleModel>> GetGuildUserRoles(ulong guildId, ulong userId)
        {
            RestGuildUser user = await _discordClient.GetGuildUserAsync(guildId, userId);
            IEnumerable<RoleModel> guildRoles = await GetGuildRoles(guildId);
            return guildRoles.Where(r => user.RoleIds.Contains(r.Id));
        }

        public async Task<IEnumerable<RoleModel>> GetGuildRoles(ulong guildId)
        {
            return (await _discordClient.GetGuildAsync(guildId)).Roles.Select(r => new RoleModel
            {
                AccessLevel = EAccessLevel.User,
                GuildId = guildId, 
                Id = r.Id,
                Name = r.Name
            });
        }

        public async Task<IEnumerable<GuildModel>> GetGuilds()
        {
            return (await _discordClient.GetGuildsAsync()).Select(g => new GuildModel
            {
                IconUrl = g.IconUrl,
                Id = g.Id,
                Name = g.Name,
                OwnerId = g.OwnerId
            });
        }

        public async Task<GuildModel> GetGuild(ulong guildId)
        {
            RestGuild data = await _discordClient.GetGuildAsync(guildId);
            return new GuildModel
            {
                IconUrl = data.IconUrl,
                Id = data.Id,
                Name = data.Name,
                OwnerId = data.OwnerId
            };
        }
        #endregion
    }
}
