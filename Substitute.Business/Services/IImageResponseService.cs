using Substitute.Business.DataStructs.ImageResponse;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Substitute.Business.Services
{
    public interface IImageResponseService
    {
        IEnumerable<ImageResponseDigestModel> List(ImageResponseFilterModel filter);
        Task<ImageResponseModel> Details(ulong id);
        Task<ulong> Create(ImageResponseModel imageResponse);
        Task<ImageResponseModel> Update(ImageResponseModel imageResponse);
        Task Delete(ulong id);
        Task<ImageResponseModel> GetImageByCommand(string command, ulong guildId);
    }
}
