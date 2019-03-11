using Substitute.Business.DataStructs.ImageResponse;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Substitute.Business.Services
{
    public interface IImageResponseService
    {
        IEnumerable<ImageResponseDigestModel> List(ImageResponseFilterModel filter);
        Task<ImageResponseModel> Details(ulong id, ulong userId, ulong? guildId = null);
        Task<ulong> Create(ImageResponseModel imageResponse, ulong userId);
        Task<ImageResponseModel> Update(ImageResponseModel imageResponse, ulong userId);
        Task Delete(ulong id, ulong userId);
        Task<ImageResponseModel> GetImageByCommand(string command, ulong guildId, ulong userId);
    }
}
