using Substitute.Domain.Enums;
using System.Threading.Tasks;

namespace Substitute.Domain.DataStore
{
    public interface IStorage
    {
        bool Exists(EStorage storage, string filename, params string[] path);
        bool Exists(EStorage storage, string filename, string path);
        bool Exists(EStorage storage, string filename);
        Task<byte[]> Get(EStorage storage, string filename, params string[] path);
        Task<byte[]> Get(EStorage storage, string filename, string path);
        Task<byte[]> Get(EStorage storage, string filename);
        Task Put(byte[] data, EStorage storage, string filename, params string[] path);
        Task Put(byte[] data, EStorage storage, string filename, string path);
        Task Put(byte[] data, EStorage storage, string filename);
        void Delete(EStorage storage, string filename, params string[] path);
        void Delete(EStorage storage, string filename, string path);
        void Delete(EStorage storage, string filename);
    }
}
