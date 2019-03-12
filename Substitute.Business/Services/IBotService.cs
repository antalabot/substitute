using System.Threading.Tasks;

namespace Substitute.Business.Services
{
    public interface IBotService
    {
        string GetJoinLink(ulong guildId, string returlUrl);
        Task<bool> HasJoined(ulong guildId);
    }
}
