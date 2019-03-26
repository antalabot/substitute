using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Substitute.Webpage.Extensions
{
    public static class DiscordUserExtensions
    {
        public static ulong GetUserId(this ClaimsPrincipal user)
        {
            return ulong.Parse(user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }

        public static string GetUserName(this ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
        }

        public static async Task<string> GetUserToken(this HttpContext context)
        {
            return await context.GetTokenAsync(context.User?.Identity?.AuthenticationType, "access_token");
        }
    }
}
