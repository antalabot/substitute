using Substitute.Domain.Enums;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Substitute.Domain.DataStore.Impl
{
    public class FileStorage : IStorage
    {
        public bool Exists(EStorage storage, string filename, params string[] path)
        {
            return File.Exists(GetPath(storage, filename, path));
        }

        public bool Exists(EStorage storage, string filename, string path)
        {
            return File.Exists(GetPath(storage, filename, path));
        }

        public bool Exists(EStorage storage, string filename)
        {
            return File.Exists(GetPath(storage, filename));
        }

        public async Task<byte[]> Get(EStorage storage, string filename, params string[] path)
        {
            return await File.ReadAllBytesAsync(GetPath(storage, filename, path));
        }

        public async Task<byte[]> Get(EStorage storage, string filename, string path)
        {
            return await File.ReadAllBytesAsync(GetPath(storage, filename, path));
        }

        public async Task<byte[]> Get(EStorage storage, string filename)
        {
            return await File.ReadAllBytesAsync(GetPath(storage, filename));
        }

        public async Task Put(byte[] data, EStorage storage, string filename, params string[] path)
        {
            await File.WriteAllBytesAsync(GetPath(storage, filename, path), data);
        }

        public async Task Put(byte[] data, EStorage storage, string filename, string path)
        {
            await File.WriteAllBytesAsync(GetPath(storage, filename, path), data);
        }

        public async Task Put(byte[] data, EStorage storage, string filename)
        {
            await File.WriteAllBytesAsync(GetPath(storage, filename), data);
        }

        public void Delete(EStorage storage, string filename, params string[] path)
        {
            File.Delete(GetPath(storage, filename, path));
        }

        public void Delete(EStorage storage, string filename, string path)
        {
            File.Delete(GetPath(storage, filename, path));
        }

        public void Delete(EStorage storage, string filename)
        {
            File.Delete(GetPath(storage, filename));
        }

        #region Private helpers
        private string GetPath(EStorage storage, string filename, params string[] path)
        {
            return GetPath(storage, filename, Path.Combine(path));
        }

        private string GetPath(EStorage storage, string filename)
        {
            return GetPath(storage, filename, string.Empty);
        }

        private string GetPath(EStorage storage, string filename, string path)
        {
            string[] parts = new string[] { storage.ToString(), path, filename }.Where(p => !string.IsNullOrWhiteSpace(p)).ToArray();
            return Path.Combine(parts);
        }
        #endregion
    }
}
