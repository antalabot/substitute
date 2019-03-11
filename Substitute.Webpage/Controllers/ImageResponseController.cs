using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Substitute.Business.DataStructs.ImageResponse;
using Substitute.Business.Services;
using System.Threading.Tasks;

namespace Substitute.Webpage.Controllers
{
    [Authorize]
    public class ImageResponseController : ControllerBase
    {
        #region Private readonly variables
        private readonly IImageResponseService _imageResponseService;
        #endregion

        #region Constructor
        public ImageResponseController(IImageResponseService imageResponseService, IUserService userService)
            : base(userService)
        {
            _imageResponseService = imageResponseService;
        }
        #endregion

        #region Views
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region POST methods
        [HttpPost]
        public JsonResult List(ImageResponseFilterModel filter)
        {
            return GetResult(() => _imageResponseService.List(filter));
        }

        [HttpPost]
        public async Task<JsonResult> Create(ImageResponseModel model)
        {
            model.GuildId = UserServer;
            return await GetResultAsync(() => _imageResponseService.Create(model, UserData.Id));
        }

        [HttpPost]
        public async Task<JsonResult> Details(ulong id)
        {
            return await GetResultAsync(() => _imageResponseService.Details(id, UserData.Id, UserServer));
        }

        [HttpPost]
        public async Task<JsonResult> Update(ImageResponseModel model)
        {
            model.GuildId = UserServer;
            return await GetResultAsync(() => _imageResponseService.Update(model, UserData.Id));
        }

        [HttpPost]
        public async Task<JsonResult> Delete(ulong id)
        {
            return await GetResultAsync(() => _imageResponseService.Delete(id, UserData.Id));
        }
        #endregion
    }
}