using System;

namespace Substitute.Domain
{
    public static class Settings
    {
        #region Private constants
        private const string DISCORD_ID_KEY = "DiscordId";
        private const string DISCORD_SECRET_KEY = "DiscordSecret";
        private const string DISCORD_TOKEN_KEY = "DiscordToken";
        private const string PG_HOST_KEY = "PgHost";
        private const string PG_DATABASE_KEY = "PgDatabase";
        private const string PG_USERNAME_KEY = "PgUsername";
        private const string PG_PASSWORD_KEY = "PgPassword";
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

        public static string PostgresHost
        {
            get
            {
                return GetSetting(PG_HOST_KEY);
            }
        }

        public static string PostgresDatabase
        {
            get
            {
                return GetSetting(PG_DATABASE_KEY);
            }
        }

        public static string PostgresUsername
        {
            get
            {
                return GetSetting(PG_USERNAME_KEY);
            }
        }

        public static string PostgresPassword
        {
            get
            {
                return GetSetting(PG_PASSWORD_KEY);
            }
        }

        public static string PostgresConnectionString
        {
            get
            {
                return string.Format("Host={0};Database={1};Username={2};Password={3}", PostgresHost, PostgresDatabase, PostgresUsername, PostgresPassword);
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
