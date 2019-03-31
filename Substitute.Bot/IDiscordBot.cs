using System.Threading.Tasks;

namespace Substitute.Bot
{
    public interface IDiscordBot
    {
        Task LoginAndStart();
    }
}
