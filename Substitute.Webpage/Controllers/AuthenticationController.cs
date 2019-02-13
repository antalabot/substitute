using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Substitute.Webpage.Extensions;
using System.Threading.Tasks;

namespace Substitute.Webpage.Controllers
{
    public class AuthenticationController : Controller
    {
        [HttpGet("~/signin")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> SignIn() => View("SignIn", await HttpContext.GetExternalProvidersAsync());

        [HttpPost("~/signin")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> SignIn([FromForm] string provider)
        {
            if (string.IsNullOrWhiteSpace(provider))
            {
                return BadRequest();
            }

            if (!await HttpContext.IsProviderSupportedAsync(provider))
            {
                return BadRequest();
            }
            
            return Challenge(new AuthenticationProperties { RedirectUri = "/" }, provider);
        }

        [HttpGet("~/signout"), HttpPost("~/signout")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult SignOut()
        {
            return SignOut(new AuthenticationProperties { RedirectUri = "/" },
                CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}