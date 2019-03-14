using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Substitute.Business.DataStructs.Guild;
using Substitute.Business.DataStructs.Role;
using Substitute.Business.Services;
using Substitute.Domain.Enums;

namespace Substitute.Webpage.Controllers
{
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly IGuildService _guildService;

        public RoleController(IGuildService guildService, IUserService userService)
            : base(userService)
        {
            _guildService = guildService;
        }

        [HttpGet]
        public async Task<IActionResult> Index() => await CheckPrivilages(EAccessLevel.Owner) ?? View();

        [HttpPost]
        public async Task<IActionResult> List(RoleFilterModel model)
        {
            model.GuildId = UserGuildId.GetValueOrDefault();
            return await CheckPrivilages(EAccessLevel.Owner) ?? await GetResultAsync(async() => await _guildService.GetRoles(model));
        }

        public async Task<IActionResult> SetAccessLevel(RoleModel model)
        {
            model.GuildId = UserGuildId.GetValueOrDefault();
            return await CheckPrivilages(EAccessLevel.Owner) ?? await GetResultAsync(async () => await _guildService.SetRoleAccessLevel(model));
        }
    }
}