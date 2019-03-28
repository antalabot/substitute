using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Substitute.Business.Services;
using Substitute.Webpage.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace Substitute.Webpage.Controllers
{
    [Authorize]
    public class GuildController : ControllerBase
    {
        private readonly IBotService _botService;

        public GuildController(IBotService botService, IUserService userService, IGuildService guildService)
            : base(userService, guildService)
        {
            _botService = botService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Choose", "Guild");
        }
        
        [HttpGet]
        public async Task<IActionResult> Choose()
        {
            return View(await _userService.GetGuilds(User.GetUserId(), await HttpContext.GetUserToken()));
        }

        [HttpGet]
        public async Task<IActionResult> Select(ulong guildId)
        {
            if (!(await _userService.GetGuilds(User.GetUserId(), await HttpContext.GetUserToken())).Any(g => g.Id == guildId && (g.CanManage || g.IsOwner)))
            {
                return Unauthorized();
            }

            if (!await _botService.HasJoined(guildId))
            {
                return Redirect(_botService.GetJoinLink(guildId, Request.GetEncodedUrl()));
            }

            SetUserGuildId(guildId);
            
            return RedirectToAction("Index", "Home");
        }
    }
}