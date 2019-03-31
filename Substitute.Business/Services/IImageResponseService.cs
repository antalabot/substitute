using Substitute.Business.DataStructs.ImageResponse;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Substitute.Business.Services
{
    public interface IImageResponseService
    {
        ImageResponseResultsModel List(ImageResponseFilterModel filter);
        Task<ImageResponseModel> Details(ulong id, string returnUrl);
        Task<ImageResponseModel> Create(ImageResponseModel imageResponse);
        Task<ImageResponseModel> Update(ImageResponseModel imageResponse);
        Task Delete(ulong id, ulong? guildId);
        Task<ImageResponseModel> GetImageByCommand(string command, ulong guildId);
    }
}
