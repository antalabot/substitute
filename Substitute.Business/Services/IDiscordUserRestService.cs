using Substitute.Business.DataStructs.Guild;
using Substitute.Business.DataStructs.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Substitute.Business.Services
{
    public interface IDiscordUserRestService : IDisposable
    {
        Task<IEnumerable<UserGuildModel>> GetGuilds();
        UserDataModel GetData();
    }
}
