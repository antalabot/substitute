using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Substitute.Business;
using Substitute.Business.Services;
using Substitute.Webpage.Extensions;
using Substitute.Webpage.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Substitute.Webpage.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly IDiscordBotRestService _discordBotRestService;

        public HomeController(IDiscordBotRestService discordBotRestService, IUserService userService)
            : base(userService)
        {
            _discordBotRestService = discordBotRestService;
        }

        public IActionResult Index()
        {
            if (IsUserAuthenticated)
            {
                return RedirectAuthenticated();
            }
            return View();
        }

        [Authorize]
        public IActionResult Setup()
        {
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
