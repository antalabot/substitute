using Discord;
using Discord.Rest;
using System;
using System.Collections.Generic;
using System.Text;

namespace Substitute.Business.Services.Impl
{
    public abstract class DiscordRestServiceBase : IDisposable
    {
        #region Private readonly variables
        protected readonly DiscordRestClient _discordClient;
        #endregion

        #region Constructor and destructor
        public DiscordRestServiceBase(TokenType tokenType, string token)
        {
            _discordClient = new DiscordRestClient();
            _discordClient.LoginAsync(tokenType, token).GetAwaiter().GetResult();
        }

        ~DiscordRestServiceBase()
        {
            try
            {
                Dispose();
            }
            catch { }
        }
        #endregion

        public void Dispose()
        {
            _discordClient.LogoutAsync().GetAwaiter().GetResult();
            _discordClient.Dispose();
        }
    }
}
