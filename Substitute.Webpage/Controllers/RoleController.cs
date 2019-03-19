using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Substitute.Business.DataStructs.Guild;
using Substitute.Business.DataStructs.Role;
using Substitute.Business.Services;
using Substitute.Domain.Enums;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Index(RoleFilterModel model) => await CheckPrivilages(EAccessLevel.Owner) ?? View(await GetResultAsync(async () => await _guildService.GetRoles(model ?? new RoleFilterModel())));
        
        [HttpPost]
        public async Task<IActionResult> SetAccessLevel(RoleModel model)
        {
            model.GuildId = UserGuildId.GetValueOrDefault();
            return await CheckPrivilages(EAccessLevel.Owner) ?? await SetAccessLevelPrivate(model);
        }

        private async Task<IActionResult> SetAccessLevelPrivate(RoleModel model)
        {
            await _guildService.SetRoleAccessLevel(model);
            return Redirect(HttpContext?.Request?.GetDisplayUrl());
        }
    }
}