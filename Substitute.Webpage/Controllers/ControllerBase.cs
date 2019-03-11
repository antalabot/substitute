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
        private const string SERVER_ID = "ServerId";
        private const string USER_DATA = "UserData";
        #endregion

        #region Private readonly variables
        private readonly object _userDataLock;
        #endregion

        #region Protected readonly variables
        protected readonly IUserService _userService;
        #endregion

        #region Constructor
        public ControllerBase(IUserService userService)
        {
            _userService = userService;
            _userDataLock = new object();
        }
        #endregion

        #region Protected helpers
        protected void SetUserServer(ulong serverId)
        {
            HttpContext.Session.Set(SERVER_ID, serverId);
        }

        protected void SetUserData(UserDataModel model)
        {
            HttpContext.Session.Set(USER_DATA, model);
        }

        protected ActionResult RedirectAuthenticated()
        {
            if (HasUserServer)
            {
                return RedirectToAction("Index", "Server");
            }
            else
            {
                return RedirectToAction("Choose", "Server");
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
        protected bool HasUserData
        {
            get
            {
                return HttpContext.HasUserData();
            }
        }

        protected UserDataModel UserData
        {
            get
            {
                if (!HasUserData)
                {
                    lock (_userDataLock)
                    {
                        if (!HasUserData)
                        {
                            SetUserData(_userService.GetUserData(User?.GetUserToken().ToString()));
                        }
                    }
                }
                return HttpContext.GetUserData();
            }
        }

        protected bool HasUserServer
        {
            get
            {
                return HttpContext.HasUserServer();
            }
        }

        protected ulong? UserServer
        {
            get
            {
                return HttpContext.GetUserServerOrNull();
            }
        }

        protected bool IsUserAuthenticated
        {
            get
            {
                return User?.Identity?.IsAuthenticated ?? false;
            }
        }
        #endregion
    }
}
