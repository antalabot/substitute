using System;

namespace Substitute.Domain
{
    public static class Settings
    {
        #region Private constants
        private const string DISCORD_ID_KEY = "DiscordId";
        private const string DISCORD_SECRET_KEY = "DiscordSecret";
        private const string DISCORD_TOKEN_KEY = "DiscordToken";
        #endregion

        #region Getters
        public static string DiscordId
        {
            get
            {
                return GetSetting(DISCORD_ID_KEY);
            }
        }

        public static string DiscordSecret
        {
            get
            {
                return GetSetting(DISCORD_SECRET_KEY);
            }
        }

        public static string DiscordToken
        {
            get
            {
                return GetSetting(DISCORD_TOKEN_KEY);
            }
        }
        #endregion

        #region Private helpers
        private static string GetSetting(string key)
        {
            return Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Machine)[key] as string;
        }
        #endregion
    }
}
