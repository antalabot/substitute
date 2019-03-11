using Discord;
using Discord.Rest;
using Substitute.Business.DataStructs.Guild;
using Substitute.Domain;
using Substitute.Domain.Data.Entities;
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

        #region Private readonly variables
        private readonly ICache _cache;
        private readonly IContext _context;

        private readonly TimeSpan _getGuildUserRolesExpiration = new TimeSpan(0, 5, 0);
        private readonly TimeSpan _getGuildRolesExpiration = new TimeSpan(0, 5, 0);
        private readonly TimeSpan _getGuildUserAccessLevelExpiration = new TimeSpan(0, 15, 0);
        #endregion

        #region Constructor and destructor
        public DiscordBotRestService(ICache cache, IContext context)
            : base(TokenType.Bot, Settings.DiscordToken)
        {
            _cache = cache;
            _context = context;
        }
        #endregion

        #region Public methods
        public async Task<IEnumerable<Role>> GetGuildUserRoles(ulong guildId, ulong userId)
        {
            return await _cache.GetOrCreateAsync($"{CLASS_NAME}|GetGuildUserRoles|{guildId}|{userId}",
                                                 entity =>
                                                 {
                                                     entity.SlidingExpiration = _getGuildUserRolesExpiration;
                                                     return FetchGuildUserRoles(guildId, userId);
                                                 });
        }

        public async Task<IEnumerable<Role>> GetGuildRoles(ulong guildId)
        {
            return await _cache.GetOrCreateAsync($"{CLASS_NAME}|GetGuildRoles|{guildId}",
                                                 entity =>
                                                 {
                                                     entity.SlidingExpiration = _getGuildRolesExpiration;
                                                     return FetchGuildRoles(guildId);
                                                 });
        }

        public async Task<EAccessLevel> GetGuildUserAccessLevel(ulong guildId, ulong userId)
        {
            return await _cache.GetOrCreateAsync($"{CLASS_NAME}|GetGuildUserAccessLevel|{guildId}|{userId}",
                                                 async entity =>
                                                 {
                                                     entity.SlidingExpiration = _getGuildUserAccessLevelExpiration;
                                                     IEnumerable<Role> roles = await GetGuildUserRoles(guildId, userId);
                                                     return roles.Min(r => r.AccessLevel);
                                                 });
        }
        #endregion

        #region Private helpers
        public async Task<IEnumerable<Role>> FetchGuildRoles(ulong guildId)
        {
            var guild = await _discordClient.GetGuildAsync(guildId);
            var dbRoles = _context.Get<GuildRole>().Where(r => r.GuildId == guildId);
            return guild.Roles.GroupJoin(dbRoles, k => k.Id, k => k.Id, (rest, db) => new Role
            {
                AccessLevel = db.SingleOrDefault()?.AccessLevel ?? EAccessLevel.User,
                Id = rest.Id,
                Name = rest.Name
            });
        }

        public async Task<IEnumerable<Role>> FetchGuildUserRoles(ulong guildId, ulong userId)
        {
            RestGuildUser user = await _discordClient.GetGuildUserAsync(guildId, userId);
            IEnumerable<Role> guildRoles = await GetGuildRoles(guildId);
            return guildRoles.Where(r => user.RoleIds.Contains(r.Id));
        }
        #endregion
    }
}
