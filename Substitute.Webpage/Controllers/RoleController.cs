using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Substitute.Business.Services;
using Substitute.Domain.Enums;

namespace Substitute.Webpage.Controllers
{
    [Authorize]
    public class RoleController : ControllerBase
    {
        public RoleController(IUserService userService)
            : base(userService)
        {

        }

        public IActionResult List()
        {
            return View();
        }

        public IActionResult SetAccessLevel(ulong roleId, EAccessLevel accessLevel)
        {
            return View();
        }
    }
}