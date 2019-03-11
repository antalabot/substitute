using System.Linq;
using System.Security.Claims;

namespace Substitute.Webpage.Extensions
{
    public static class DiscordUserExtensions
    {
        private const string NAME_IDENTIFIER = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

        public static ulong GetUserToken(this ClaimsPrincipal user)
        {
            return ulong.Parse(user.Claims.FirstOrDefault(c => c.Type == NAME_IDENTIFIER).Value);
        }
    }
}
