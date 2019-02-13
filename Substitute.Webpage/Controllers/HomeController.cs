using Microsoft.AspNetCore.Mvc;
using Substitute.Business;
using Substitute.Webpage.Extensions;
using Substitute.Webpage.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Substitute.Webpage.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AboutMe()
        {
            return View(await DiscordRest.Instance.GetUserData(User.GetUserId()));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
