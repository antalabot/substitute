using Substitute.Business.DataStructs.Guild;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Substitute.Business.Services
{
    public interface IDiscordUserRestService
    {
        Task<IEnumerable<UserGuildModel>> GetGuilds();
    }
}
