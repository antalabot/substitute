using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Substitute.Business.DataStructs.User;
using Substitute.Business.Services;
using Substitute.Domain.Enums;
using Substitute.Webpage.Extensions;
using Substitute.Webpage.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Substitute.Webpage.Controllers
{
    public abstract class ControllerBase : Controller
    {
        #region Private constants
        private const string GUILD_ID = "GuildId";
        private const string GUILD_DATA = "GuildData";
        private const string USER_DATA = "UserData";
        #endregion

        #region Protected readonly variables
        protected readonly IUserService _userService;
        protected readonly IGuildService _guildService;
        #endregion

        #region Constructor
        public ControllerBase(IUserService userService, IGuildService guildService)
        {
            _userService = userService;
            _guildService = guildService;
        }
        #endregion

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                ViewBag.UserData = GetUserData().GetAwaiter().GetResult();
                if (HasUserGuildId)
                {
                    ViewBag.GuildData = _userService.GetGuildData(User.GetUserId(), HttpContext.GetUserToken().GetAwaiter().GetResult(), UserGuildId.GetValueOrDefault()).GetAwaiter().GetResult();
                }
            }
        }

        #region Protected helpers
        protected void SetUserGuildId(ulong serverId) => HttpContext.Session.Set(GUILD_ID, serverId);

        protected void SetUserData(UserDataModel model) => HttpContext.Session.Set(USER_DATA, model);

        protected async Task<UserDataModel> GetUserData()
        {
            if (!HasUserData)
            {
                if (!HasUserData)
                {
                    SetUserData(await _userService.GetUserData(User.GetUserId(), await HttpContext.GetUserToken()));
                }
            }
            return HttpContext.GetUserData();
        }

        protected ActionResult RedirectToGuildSelect()
        {
            return RedirectToAction("Choose", "Guild");
        }

        protected ActionResult RedirectAuthenticated()
        {
            if (HasUserGuildId)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToGuildSelect();
            }
        }

        protected JsonResult GetResult<T>(Func<T> func)
        {
            try
            {
                return Json(new JsonResponse(func()));
            }
            catch (Exception ex)
            {
                return Json(JsonResponse.FromException(ex));
            }
        }

        protected JsonResult GetResult(Action func)
        {
            try
            {
                func();
                return Json(new JsonResponse { Success = true });
            }
            catch (Exception ex)
            {
                return Json(JsonResponse.FromException(ex));
            }
        }

        protected async Task<JsonResult> GetResultAsync<T>(Func<Task<T>> func)
        {
            try
            {
                return Json(new JsonResponse(await func()));
            }
            catch (Exception ex)
            {
                return Json(JsonResponse.FromException(ex));
            }
        }

        protected async Task<JsonResult> GetResultAsync(Func<Task> func)
        {
            try
            {
                await func();
                return Json(new JsonResponse { Success = true });
            }
            catch (Exception ex)
            {
                return Json(JsonResponse.FromException(ex));
            }
        }

        protected async Task CheckRole(ERole role)
        {
            ERole userRole = (await GetUserData()).Role;
            switch (role)
            {
                case ERole.Owner:
                    if (!new ERole[] { ERole.Owner }.Contains(userRole))
                    {
                        throw new UnauthorizedAccessException();
                    }
                    break;
                case ERole.User:
                    if (!new ERole[] { ERole.Owner, ERole.User }.Contains(userRole))
                    {
                        throw new UnauthorizedAccessException();
                    }
                    break;
            }
        }

        protected async Task<IActionResult> CheckPrivilages(EAccessLevel accessLevel)
        {
            if (!IsUserAuthenticated)
            {
                return Redirect("/signin");
            }
            
            if (User == null)
            {
                return BadRequest();
            }

            if (!HasUserGuildId)
            {
                return RedirectToGuildSelect();
            }

            if (!await HasGuildAccessLevel(User.GetUserId(), UserGuildId.GetValueOrDefault(), accessLevel))
            {
                return Unauthorized();
            }
            return null;
        }

        protected async Task<bool> HasGuildAccessLevel(ulong userId, ulong guildId, EAccessLevel accessLevel)
        {
            EAccessLevel userAccessLevel = await _userService.GetGuildAccessLevel(userId, await HttpContext.GetUserToken(), guildId);
            switch (accessLevel)
            {
                case EAccessLevel.Administrator:
                    return new EAccessLevel[] { EAccessLevel.Administrator, EAccessLevel.Owner }.Contains(userAccessLevel);
                case EAccessLevel.Moderator:
                    return new EAccessLevel[] { EAccessLevel.Moderator, EAccessLevel.Administrator, EAccessLevel.Owner }.Contains(userAccessLevel);
                case EAccessLevel.Owner:
                    return new EAccessLevel[] { EAccessLevel.Owner }.Contains(userAccessLevel);
                case EAccessLevel.User:
                    return new EAccessLevel[] { EAccessLevel.User, EAccessLevel.Moderator, EAccessLevel.Administrator, EAccessLevel.Owner }.Contains(userAccessLevel);
                default:
                    return false;
            }
        }
        #endregion

        #region Protected properties
        protected bool HasUserData => HttpContext.HasUserData();
        
        protected bool HasUserGuildId => HttpContext.HasUserGuildId();

        protected ulong? UserGuildId => HttpContext.GetUserGuildIdOrNull();

        protected bool IsUserAuthenticated => User?.Identity?.IsAuthenticated ?? false;
        #endregion
    }
}
