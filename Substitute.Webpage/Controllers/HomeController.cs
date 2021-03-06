﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Substitute.Business.Services;
using Substitute.Webpage.Extensions;
using Substitute.Webpage.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Substitute.Webpage.Controllers
{
    public class HomeController : ControllerBase
    {
        public HomeController(IUserService userService, IGuildService guildService)
            : base(userService, guildService)
        {
        }

        public IActionResult Index()
        {
            if (IsUserAuthenticated && !HttpContext.HasUserGuildId())
            {
                return RedirectAuthenticated();
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Setup()
        {
            if (await _userService.IsOwnerSet())
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> ClaimOwnership()
        {
            if (await _userService.IsOwnerSet())
            {
                return RedirectToAction("Index", "Home");
            }
            await _userService.SetOwner(User.GetUserId(), await HttpContext.GetUserToken());
            return RedirectToAction("Index", "Home");
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
