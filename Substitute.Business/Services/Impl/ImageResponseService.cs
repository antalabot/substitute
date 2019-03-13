using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Substitute.Business.DataStructs.Exceptions;
using Substitute.Business.DataStructs.ImageResponse;
using Substitute.Domain.Data;
using Substitute.Domain.Data.Entities;
using Substitute.Domain.DataStore;
using Substitute.Domain.Enums;

namespace Substitute.Business.Services.Impl
{
    public class ImageResponseService : IImageResponseService
    {
        #region Private readonly variables
        private readonly ICache _cache;
        private readonly IContext _context;
        private readonly IStorage _storage;
        private readonly ISnowflake _snowflake;
        #endregion

        #region Constructor
        public ImageResponseService(IContext context, ICache cache, IStorage storage, ISnowflake snowflake)
        {
            _context = context;
            _cache = cache;
            _storage = storage;
            _snowflake = snowflake;
        }
        #endregion

        public async Task<ulong> Create(ImageResponseModel imageResponse)
        {
            bool exists = _context.Get<ImageResponse>().Any(r => r.Command == imageResponse.Command);
            if (exists)
            {
                throw new CommandExistsException();
            }
            
            string filename = $"{imageResponse.Command}{Path.GetExtension(imageResponse.Filename)}";
            ImageResponse model = new ImageResponse
            {
                Command = imageResponse.Command,
                GuildId = imageResponse.GuildId,
                Id = _snowflake.GetSnowflake(),
                Fielename = filename
            };

            _context.Add(model);

            await _storage.Put(imageResponse.Image, EStorage.ImageResponse, filename, imageResponse.GuildId.ToString());

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _storage.Delete(EStorage.ImageResponse, filename, imageResponse.GuildId.ToString());
                throw ex;
            }

            return model.Id;
        }

        public async Task Delete(ulong id)
        {
            ImageResponse image = await _context.GetByIdAsync<ImageResponse>(id);
            if (image == null)
            {
                throw new CommandNotExistsException();
            }

            await _context.RemoveByIdAsync<ImageResponse>(id);
            _context.SaveChanges();
            _storage.Delete(EStorage.ImageResponse, image.Fielename, image.GuildId.ToString());
        }

        public async Task<ImageResponseModel> Details(ulong id)
        {
            ImageResponse image = await _context.GetByIdAsync<ImageResponse>(id);
            if (image == null)
            {
                throw new CommandNotExistsException();
            }

            return await GetModel(image);
        }

        public async Task<ImageResponseModel> GetImageByCommand(string command, ulong guildId)
        {
            ImageResponse image = _context.Get<ImageResponse>().SingleOrDefault(r => r.Command == command && r.GuildId == guildId);
            if (image == null)
            {
                throw new CommandNotExistsException();
            }
            
            return await GetModel(image);
        }

        public IEnumerable<ImageResponseDigestModel> List(ImageResponseFilterModel filter)
        {
            ulong guildId = filter.GuildId.GetValueOrDefault();

            IQueryable<ImageResponse> images = _context.Get<ImageResponse>();

            if (filter.GuildId.HasValue)
            {
                images = images.Where(i => i.GuildId == guildId);
            }

            if (!string.IsNullOrWhiteSpace(filter.Command))
            {
                images = images.Where(i => i.Command.Contains(filter.Command));
            }

            return images.Skip(filter.PerPage * (filter.Page - 1))
                         .Take(filter.PerPage)
                         .Select(i => new ImageResponseDigestModel
                         {
                             Command = i.Command,
                             Filename = i.Fielename,
                             Id = i.Id
                         }).ToArray();
        }

        public async Task<ImageResponseModel> Update(ImageResponseModel imageResponse)
        {
            ImageResponse model = await _context.GetByIdAsync<ImageResponse>(imageResponse.Id);
            if (model == null)
            {
                throw new CommandNotExistsException();
            }
            
            string filename = $"{imageResponse.Command}{Path.GetExtension(imageResponse.Filename)}";
            byte[] data = imageResponse.Image;
            string guildIdString = model.GuildId.GetValueOrDefault().ToString();

            if (imageResponse.Command != model.Command)
            {
                if (data == null)
                {
                    data = await _storage.Get(EStorage.ImageResponse, model.Fielename, guildIdString);
                }

                await _storage.Put(data, EStorage.ImageResponse, filename, guildIdString);
                _storage.Delete(EStorage.ImageResponse, model.Fielename, guildIdString);

                model.Fielename = filename;
                model.Command = imageResponse.Command;

                _context.SaveChanges();

                imageResponse.Filename = filename;
            }
            else if (imageResponse.Image != null)
            {
                _storage.Delete(EStorage.ImageResponse, model.Fielename, guildIdString);
                await _storage.Put(data, EStorage.ImageResponse, model.Fielename, guildIdString);
            }

            return imageResponse;
        }

        #region Private helpers
        private async Task<ImageResponseModel> GetModel(ImageResponse image)
        {
            return new ImageResponseModel
            {
                Command = image.Command,
                Filename = image.Fielename,
                GuildId = image.GuildId,
                Id = image.Id,
                Image = await _storage.Get(EStorage.ImageResponse, image.Fielename, image.GuildId.GetValueOrDefault().ToString())
            };
        }
        #endregion
    }
}
