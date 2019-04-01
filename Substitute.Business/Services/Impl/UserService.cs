using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Substitute.Business.DataStructs.Guild;
using Substitute.Business.DataStructs.User;
using Substitute.Domain.Data.Entities;
using Substitute.Domain.DataStore;
using Substitute.Domain.Enums;

namespace Substitute.Business.Services.Impl
{
    public class UserService : IUserService
    {
        #region Private constants
        private const string CLASS_NAME = "USER";
        #endregion

        private readonly IGuildService _guildService;
        private readonly IDiscordBotRestService _botService;
        private readonly ICache _cache;
        private readonly IContext _context;

        private readonly TimeSpan _getGuildsExpiration = TimeSpan.FromMinutes(5);
        private readonly TimeSpan _getGuildUserAccessLevelExpiration = TimeSpan.FromMinutes(1);

        public UserService(IGuildService guildService, IDiscordBotRestService botService, ICache cache, IContext context)
        {
            _guildService = guildService;
            _botService = botService;
            _cache = cache;
            _context = context;
        }

        public async Task<EAccessLevel> GetGuildAccessLevel(ulong userId, string token, ulong guildId)
        {
            return await _cache.GetOrCreateAsync($"{CLASS_NAME}|GetGuildAccessLevel|{guildId}|{userId}",
                                                 async entity =>
                                                 {
                                                     entity.SlidingExpiration = _getGuildUserAccessLevelExpiration;

                                                     using (IDiscordUserRestService userService = new DiscordUserRestService(token))
                                                     {
                                                         if ((await userService.GetGuilds()).Any(g => g.Id == guildId && g.IsOwner))
                                                         {
                                                             return EAccessLevel.Owner;
                                                         }
                                                     }

                                                     IEnumerable<RoleModel> userGuildRoles = await _botService.GetGuildUserRoles(guildId, userId);
                                                     IEnumerable<GuildRole> guildRoles = (await _context.GetAsync<GuildRole>()).Where(r => r.GuildId == guildId);
                                                     IEnumerable<EAccessLevel> accessLevels = userGuildRoles.Join(guildRoles, ugr => ugr.Id, gr => gr.Id, (ugr, gr) => gr.AccessLevel);
                                                     if (!accessLevels.Any())
                                                     {
                                                         return EAccessLevel.User;
                                                     }
                                                     return accessLevels.Min();
                                                 });
        }

        public async Task<DataStructs.User.UserGuildModel> GetGuildData(ulong userId, string token, ulong guildId)
        {
            EAccessLevel accessLevel = await GetGuildAccessLevel(userId, token, guildId);
            var guildData = await _guildService.GetGuildData(guildId);
            return new DataStructs.User.UserGuildModel
            {
                AccessLevel = accessLevel,
                IconUrl = guildData.IconUrl,
                Id = guildData.Id,
                Name = guildData.Name
            };
        }

        public async Task<IEnumerable<DataStructs.Guild.UserGuildModel>> GetGuilds(ulong userId, string token)
        {
            return await _cache.GetOrCreateAsync($"{CLASS_NAME}|GetGuilds|{userId}", async entity =>
            {
                using (IDiscordUserRestService userService = new DiscordUserRestService(token))
                {
                    entity.SlidingExpiration = _getGuildsExpiration;
                    IEnumerable<DataStructs.Guild.UserGuildModel> userGuilds = await userService.GetGuilds();
                    IEnumerable<GuildModel> botGuilds = await _botService.GetGuilds();
                    return userGuilds.Where(g => g.IsOwner || g.CanManage || botGuilds.Any(b => b.Id == g.Id));
                }
            });
        }

        public async Task<UserDataModel> GetUserData(ulong userId, string token)
        {
            return await _cache.GetOrCreateAsync($"{CLASS_NAME}|GetUserData|{userId}", async entity =>
            {
                using (IDiscordUserRestService userService = new DiscordUserRestService(token))
                {
                    entity.SlidingExpiration = _getGuildsExpiration;
                    UserDataModel data = userService.GetData();
                    User user = await _context.GetByIdAsync<User>(userId);
                    if (user != null)
                    {
                        data.Role = user.Role;
                    }
                    return data;
                }
            });
        }

        public async Task<bool> IsOwnerSet()
        {
            return (await _context.GetAsync<User>()).Any(u => u.Role == ERole.Owner);
        }

        public async Task SetOwner(ulong userId, string token)
        {
            User user = await _context.GetByIdAsync<User>(userId);
            if (user == null)
            {
                user = new User
                {
                    Id = userId
                };
                _context.Add(user);
            }
            user.Role = ERole.Owner;
            _context.SaveChanges();
        }
    }
}
