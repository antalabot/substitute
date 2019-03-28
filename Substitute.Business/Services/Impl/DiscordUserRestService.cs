using Discord;
using Discord.Rest;
using Substitute.Business.DataStructs.Guild;
using Substitute.Business.DataStructs.User;
using Substitute.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Substitute.Business.Services.Impl
{
    public class DiscordUserRestService : DiscordRestServiceBase, IDiscordUserRestService, IDisposable
    {
        #region Private readonly variables
        private readonly string _token;
        #endregion

        #region Constructor
        public DiscordUserRestService(string token)
            : base(TokenType.Bearer, token)
        {
            _token = token;
        }
        #endregion

        #region Implementation of IDiscordUserRestService
        public async Task<IEnumerable<DataStructs.Guild.UserGuildModel>> GetGuilds()
        {
            IEnumerable<RestUserGuild> list = await _discordClient.GetGuildSummariesAsync().FlattenAsync();
            return list.Select(g => new DataStructs.Guild.UserGuildModel
            {
                CanManage = g.Permissions.ManageGuild,
                IconUrl = g.IconUrl,
                Id = g.Id,
                IsAdministrator = g.Permissions.Administrator,
                IsOwner = g.IsOwner,
                Name = g.Name
            });
        }

        public UserDataModel GetData()
        {
            var data = _discordClient.CurrentUser;
            return new UserDataModel
            {
                AvatarUrl = data.GetAvatarUrl(),
                Id = data.Id,
                Name = data.Mention,
                Role = ERole.User
            };
        }
        #endregion
    }
}
