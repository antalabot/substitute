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
            : base(userService) => _imageResponseService = imageResponseService;
        #endregion

        #region Views
        [HttpGet]
        public IActionResult Index() => View();
        #endregion

        #region POST methods
        [HttpPost]
        public async Task<JsonResult> List(ImageResponseFilterModel filter) => await GetResultAsync(async () => await _imageResponseService.List(filter));

        [HttpPost]
        public async Task<JsonResult> Create(ImageResponseModel model)
        {
            model.GuildId = UserGuildId;
            return await GetResultAsync(async () => _imageResponseService.Create(model, (await GetUserData()).Id));
        }

        [HttpPost]
        public async Task<JsonResult> Details(ulong id) => await GetResultAsync(async () => _imageResponseService.Details(id, (await GetUserData()).Id, UserGuildId));

        [HttpPost]
        public async Task<JsonResult> Update(ImageResponseModel model)
        {
            model.GuildId = UserGuildId;
            return await GetResultAsync(async () => _imageResponseService.Update(model, (await GetUserData()).Id));
        }

        [HttpPost]
        public async Task<JsonResult> Delete(ulong id) => await GetResultAsync(async () => _imageResponseService.Delete(id, (await GetUserData()).Id));
        #endregion
    }
}