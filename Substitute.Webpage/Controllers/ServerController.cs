using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Substitute.Webpage.Controllers
{
    [Authorize]
    public class ServerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Choose()
        {
            return View();
        }

        public IActionResult Choose(ulong serverId)
        {
            return View();
        }
    }
}