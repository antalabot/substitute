using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Substitute.Business.DataStructs.ImageResponse;
using Substitute.Business.Services;
using Substitute.Domain.Enums;
using System;
using System.IO;
using System.Runtime.InteropServices;
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
        public ImageResponseController(IImageResponseService imageResponseService, IUserService userService, IGuildService guildService)
            : base(userService, guildService) => _imageResponseService = imageResponseService;
        #endregion

        #region Views
        [HttpGet]
        public async Task<IActionResult> Index(ImageResponseFilterModel filter)
        {
            filter = filter ?? new ImageResponseFilterModel();
            filter.GuildId = UserGuildId.GetValueOrDefault();
            return await CheckPrivilages(EAccessLevel.User) ?? View(_imageResponseService.List(filter));
        }

        [HttpGet]
        public async Task<IActionResult> Details(ulong id, string returnUrl) => await CheckPrivilages(EAccessLevel.User) ?? View(await _imageResponseService.Details(id, returnUrl));

        [HttpGet]
        public async Task<IActionResult> Image(ulong id, [Optional] string filename) => await CheckPrivilages(EAccessLevel.User) ?? await GetResponseImage(id);
        #endregion

        #region POST methods
        [HttpPost]
        public async Task<IActionResult> Create(ImageResponseModel model) => await CheckPrivilages(EAccessLevel.Moderator) ?? await CreateOrUpdateImgeResponse(model, _imageResponseService.Create);

        [HttpPost]
        public async Task<IActionResult> Update(ImageResponseModel model) => await CheckPrivilages(EAccessLevel.Moderator) ?? await CreateOrUpdateImgeResponse(model, _imageResponseService.Update);

        [HttpPost]
        public async Task<IActionResult> Delete(ulong id, string returnUrl) => await CheckPrivilages(EAccessLevel.Moderator) ?? await DeleteImageResponse(id, returnUrl);
        #endregion

        #region Private helpers
        private async Task<IActionResult> CreateOrUpdateImgeResponse(ImageResponseModel model, Func<ImageResponseModel, Task<ImageResponseModel>> func)
        {
            if (Request.Form.Files?.Count == 1)
            {
                IFormFile file = Request.Form.Files[0];
                using (MemoryStream stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    model.Image = stream.ToArray();
                }

                model.Filename = file.FileName;
            }
            model.GuildId = UserGuildId;

            ImageResponseModel result = await func(model);

            return RedirectToAction("Details", new { id = result.Id, returnUrl = model.ReturnUrl });
        }

        private async Task<IActionResult> DeleteImageResponse(ulong id, string returnUrl)
        {
            await _imageResponseService.Delete(id, UserGuildId);
            return Redirect(returnUrl);
        }

        private async Task<FileResult> GetResponseImage(ulong id)
        {
            FileExtensionContentTypeProvider contentTypeProvider = new FileExtensionContentTypeProvider();
            ImageResponseModel details = await _imageResponseService.Details(id, string.Empty);
            if (!contentTypeProvider.TryGetContentType(details.Filename, out string contentType))
            {
                contentType = "application/octet-stream";
            }
            return File(details.Image, contentType);
        }
        #endregion
    }
}