using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Substitute.Business.DataStructs.User;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Substitute.Webpage.Extensions
{
    public static class HttpContextExtensions
    {
        #region Private constants
        private const string SERVER_ID = "ServerId";
        private const string USER_DATA = "UserData";
        #endregion

        public static async Task<AuthenticationScheme[]> GetExternalProvidersAsync(this HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var schemes = context.RequestServices.GetRequiredService<IAuthenticationSchemeProvider>();

            return (from scheme in await schemes.GetAllSchemesAsync()
                    where !string.IsNullOrEmpty(scheme.DisplayName)
                    select scheme).ToArray();
        }

        public static async Task<bool> IsProviderSupportedAsync(this HttpContext context, string provider)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return (from scheme in await context.GetExternalProvidersAsync()
                    where string.Equals(scheme.Name, provider, StringComparison.OrdinalIgnoreCase)
                    select scheme).Any();
        }

        public static bool HasUserServer(this HttpContext context)
        {
            return context.Session.Keys.Contains(SERVER_ID);
        }

        public static ulong? GetUserServerOrNull(this HttpContext context)
        {
            return context.HasUserServer() ? context.Session.Get<ulong>(SERVER_ID) as ulong? : null;
        }

        public static bool HasUserData(this HttpContext context)
        {
            return context.Session.Keys.Contains(USER_DATA);
        }

        public static UserDataModel GetUserData(this HttpContext context)
        {
            return context.HasUserData() ? context.Session.Get<UserDataModel>(USER_DATA) as UserDataModel : null;
        }
    }
}
