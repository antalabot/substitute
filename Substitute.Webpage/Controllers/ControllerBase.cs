using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Substitute.Business.DataStructs.User;
using Substitute.Business.Services;
using Substitute.Webpage.Extensions;
using Substitute.Webpage.Models;
using System;
using System.Threading.Tasks;

namespace Substitute.Webpage.Controllers
{
    public abstract class ControllerBase : Controller
    {
        #region Private constants
        private const string GUILD_ID = "GuildId";
        private const string USER_DATA = "UserData";
        #endregion
        
        #region Protected readonly variables
        protected readonly IUserService _userService;
        #endregion

        #region Constructor
        public ControllerBase(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region Protected helpers
        protected void SetUserGuildId(ulong serverId) => HttpContext.Session.Set(GUILD_ID, serverId);

        protected void SetUserData(UserDataModel model) => HttpContext.Session.Set(USER_DATA, model);

        protected async Task<UserDataModel> GetUserData()
        {
            if (!HasUserData)
            {
                if (!HasUserData)
                {
                    SetUserData(await _userService.GetUserData(User?.GetUserToken().ToString()));
                }
            }
            return HttpContext.GetUserData();
        }

        protected ActionResult RedirectAuthenticated()
        {
            if (HasUserGuildId)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Choose", "Guild");
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
        #endregion

        #region Protected properties
        protected bool HasUserData => HttpContext.HasUserData();
        
        protected bool HasUserGuildId => HttpContext.HasUserGuildId();

        protected ulong? UserGuildId => HttpContext.GetUserGuildIdOrNull();

        protected bool IsUserAuthenticated => User?.Identity?.IsAuthenticated ?? false;
        #endregion
    }
}
