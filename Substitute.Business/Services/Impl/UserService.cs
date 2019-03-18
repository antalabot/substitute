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

        private readonly IDiscordBotRestService _botService;
        private readonly ICache _cache;
        private readonly IContext _context;

        private readonly TimeSpan _getGuildsExpiration = TimeSpan.FromMinutes(5);
        private readonly TimeSpan _getGuildUserAccessLevelExpiration = TimeSpan.FromMinutes(15);

        public UserService(IDiscordBotRestService botService, ICache cache, IContext context)
        {
            _botService = botService;
            _cache = cache;
            _context = context;
        }

        public async Task<EAccessLevel> GetGuildAccessLevel(ulong userId, ulong guildId)
        {
            return await _cache.GetOrCreateAsync($"{CLASS_NAME}|GetGuildAccessLevel|{guildId}|{userId}",
                                                 async entity =>
                                                 {
                                                     entity.SlidingExpiration = _getGuildUserAccessLevelExpiration;

                                                     IEnumerable<RoleModel> userGuildRoles = await _botService.GetGuildUserRoles(guildId, userId);
                                                     IEnumerable<GuildRole> guildRoles = (await _context.GetAsync<GuildRole>()).Where(r => r.GuildId == guildId);
                                                     return userGuildRoles.Join(guildRoles, ugr => ugr.Id, gr => gr.Id, (ugr, gr) => ugr.AccessLevel).Min();
                                                 });
        }

        public async Task<IEnumerable<UserGuildModel>> GetGuilds(string token)
        {
            return await _cache.GetOrCreateAsync($"{CLASS_NAME}|GetGuilds|{token}", async entity =>
            {
                using (IDiscordUserRestService userService = new DiscordUserRestService(token))
                {
                    entity.SlidingExpiration = _getGuildsExpiration;
                    IEnumerable<UserGuildModel> userGuilds = await userService.GetGuilds();
                    IEnumerable<GuildModel> botGuilds = await _botService.GetGuilds();
                    return userGuilds.Where(g => g.IsOwner || g.CanManage || botGuilds.Any(b => b.Id == g.Id));
                }
            });
        }

        public async Task<UserDataModel> GetUserData(string token)
        {
            return await _cache.GetOrCreateAsync($"{CLASS_NAME}|GetGuilds|{token}", async entity =>
            {
                using (IDiscordUserRestService userService = new DiscordUserRestService(token))
                {
                    entity.SlidingExpiration = _getGuildsExpiration;
                    UserDataModel data = userService.GetData();
                    User user = await _context.GetByIdAsync<User>(data.Id);
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
            }
            user.Role = ERole.Owner;
            _context.SaveChanges();
        }
    }
}
