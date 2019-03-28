using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Substitute.Business.DataStructs.ImageResponse;
using Substitute.Business.Services;
using Substitute.Domain.Enums;
using System.Threading.Tasks;
using System.Web.Http;

namespace Substitute.Webpage.Controllers
{
    [Authorize]
    public class ImageResponseController : ControllerBase
    {
        #region Private readonly variables
        private readonly IImageResponseService _imageResponseService;
        #endregion

        #region Constructor
        public ImageResponseController(IImageResponseService imageResponseService, IUserService userService, IGuildService guildService)
            : base(userService, guildService) => _imageResponseService = imageResponseService;
        #endregion

        #region Views
        [HttpGet]
        public async Task<IActionResult> Index() => await CheckPrivilages(EAccessLevel.User) ?? View();
        #endregion

        #region POST methods
        [HttpPost]
        public async Task<IActionResult> List(ImageResponseFilterModel filter) => await CheckPrivilages(EAccessLevel.User) ?? GetResult(() => _imageResponseService.List(filter));

        [HttpPost]
        public async Task<IActionResult> Create(ImageResponseModel model)
        {
            model.GuildId = UserGuildId;
            return await CheckPrivilages(EAccessLevel.Moderator) ?? await GetResultAsync(async () => await _imageResponseService.Create(model));
        }

        [HttpPost]
        public async Task<IActionResult> Details(ulong id) => await CheckPrivilages(EAccessLevel.User) ?? await GetResultAsync(async () => await _imageResponseService.Details(id));

        [HttpPost]
        public async Task<IActionResult> Update(ImageResponseModel model)
        {
            model.GuildId = UserGuildId;
            return await CheckPrivilages(EAccessLevel.Moderator) ?? await GetResultAsync(async () => await _imageResponseService.Update(model));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ulong id) => await CheckPrivilages(EAccessLevel.Moderator) ?? await GetResultAsync(async () => await _imageResponseService.Delete(id));
        #endregion
    }
}