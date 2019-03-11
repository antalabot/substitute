using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Substitute.Business.Services;
using Substitute.Webpage.Extensions;
using System.Threading.Tasks;

namespace Substitute.Webpage.Controllers
{
    public class AuthenticationController : ControllerBase
    {
        public AuthenticationController(IUserService userService)
            : base(userService)
        {

        }

        [AllowAnonymous]
        [HttpGet("~/signin")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> SignIn(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectAuthenticated();
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(await HttpContext.GetExternalProvidersAsync());
        }

        [AllowAnonymous]
        [HttpPost("~/signin")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> SignIn([FromForm] string provider, [FromForm] string returnUrl)
        {
            if (IsUserAuthenticated)
            {
                return RedirectAuthenticated();
            }

            if (string.IsNullOrWhiteSpace(provider))
            {
                return BadRequest();
            }

            if (!await HttpContext.IsProviderSupportedAsync(provider))
            {
                return BadRequest();
            }
            
            return Challenge(new AuthenticationProperties { RedirectUri = string.IsNullOrWhiteSpace(returnUrl) ? Url.Action("Choose", "Server") : returnUrl }, provider);
        }

        [Authorize]
        [HttpGet("~/signout"), HttpPost("~/signout")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult SignOut()
        {
            return SignOut(new AuthenticationProperties { RedirectUri = "/" },
                CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}