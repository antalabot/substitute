using Substitute.Domain;

namespace Substitute.Business.Services.Impl
{
    public class BotService : IBotService
    {
        private const ulong PERMISSIONS = 8;

        public string GetJoinLink(ulong guildId, string returnUrl)
        {
            return $"https://discordapp.com/oauth2/authorize?client_id={Settings.DiscordId}&scope=bot&permissions={PERMISSIONS}&redirect_url={returnUrl}";
        }
    }
}
