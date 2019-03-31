using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Substitute.Business.DataStructs.Guild;
using Substitute.Business.DataStructs.Role;
using Substitute.Business.Services;
using Substitute.Domain.Enums;
using Substitute.Webpage.Extensions;
using System.Threading.Tasks;

namespace Substitute.Webpage.Controllers
{
    [Authorize]
    public class RoleController : ControllerBase
    {
        public RoleController(IUserService userService, IGuildService guildService)
            : base(userService, guildService)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Index(RoleFilterModel model)
        {
            model = model ?? new RoleFilterModel();
            model.GuildId = UserGuildId.GetValueOrDefault();
            model.UserId = User.GetUserId();
            return await CheckPrivilages(EAccessLevel.Owner) ?? View(await _guildService.GetRoles(model));
        }
        
        [HttpPost]
        public async Task<IActionResult> SetAccessLevel(RoleModel model)
        {
            model.GuildId = UserGuildId.GetValueOrDefault();
            return await CheckPrivilages(EAccessLevel.Owner) ?? await SetAccessLevelPrivate(model);
        }

        private async Task<IActionResult> SetAccessLevelPrivate(RoleModel model)
        {
            await _guildService.SetRoleAccessLevel(model);
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}