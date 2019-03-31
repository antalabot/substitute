using System;
using System.Collections.Generic;
using System.Text;
using Substitute.Business.DataStructs.Enum;

namespace Substitute.Business.DataStructs.ImageResponse
{
    public class ImageResponseResultsModel : ResultBase, IImageResponseFilter
    {
        public ImageResponseResultsModel(ImageResponseFilterModel filter)
            : base(filter)
        {
            GuildId = filter.GuildId;
            Command = filter.Command;
            UserId = filter.UserId;
            SortBy = filter.SortBy;
        }

        public string Command { get; private set; }
        public ulong? GuildId { get; private set; }
        public ulong UserId { get; private set; }
        public EImageResponseSort SortBy { get; private set; }

        public IEnumerable<ImageResponseDigestModel> Items { get; set; }

        public override FilterBase ChangePage(int page)
        {
            return new ImageResponseFilterModel
            {
                Command = Command,
                Page = page,
                PerPage = PerPage,
                SortBy = SortBy,
                SortDirection = SortDirection
            };
        }
    }
}
