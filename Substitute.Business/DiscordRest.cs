using Discord;
using Discord.Rest;
using Substitute.Domain;
using System.Threading.Tasks;

namespace Substitute.Business
{
    public class DiscordRest
    {
        #region Singleton variables
        private static readonly object _instanceLock = new object();
        private static DiscordRest _instance;
        #endregion

        #region Private readonly variables
        private readonly DiscordRestClient _discordClient;
        #endregion

        #region Constructor and destructor
        private DiscordRest()
        {
            _discordClient = new DiscordRestClient();
            _discordClient.LoginAsync(TokenType.Bot, Settings.DiscordToken).GetAwaiter().GetResult();
        }

        ~DiscordRest()
        {
            _discordClient.LogoutAsync().GetAwaiter().GetResult();
            _discordClient.Dispose();
        }
        #endregion

        #region Instance
        public static DiscordRest Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new DiscordRest();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region Public methods
        public async Task<IUser> GetUserData(ulong id)
        {
            return await _discordClient.GetUserAsync(id);
        }
        #endregion
    }
}
