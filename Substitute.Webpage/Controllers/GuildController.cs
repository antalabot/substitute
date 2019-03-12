using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Substitute.Business.Services;
using Substitute.Webpage.Extensions;

namespace Substitute.Webpage.Controllers
{
    [Authorize]
    public class GuildController : ControllerBase
    {
        private readonly IBotService _botService;

        public GuildController(IBotService botService, IUserService userService)
            : base(userService)
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
            return View(await _userService.GetGuilds(User?.GetUserToken().ToString()));
        }

        [HttpGet]
        public async Task<IActionResult> Choose(ulong guildId)
        {
            if (!(await _userService.GetGuilds(User?.GetUserToken().ToString())).Any(g => g.Id == guildId))
            {
                throw new UnauthorizedAccessException();
            }

            if (!await _botService.HasJoined(guildId))
            {
                return Redirect(_botService.GetJoinLink(guildId, Url.Action("Choose", "Guild", new { guildId })));
            }

            SetUserGuildId(guildId);

            return RedirectToAction("Index", "Home");
        }
    }
}