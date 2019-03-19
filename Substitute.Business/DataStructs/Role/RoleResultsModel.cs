using System;
using System.Collections.Generic;
using System.Linq;
using Substitute.Business.DataStructs.Enum;
using Substitute.Domain.Enums;

namespace Substitute.Business.DataStructs.Role
{
    public class RoleResultsModel : ResultBase, IRoleFilter
    {
        public RoleResultsModel(RoleFilterModel filter)
            : base(filter)
        {
            GuildId = filter.GuildId;
            Name = filter.Name;
            AccessLevel = filter.AccessLevel;
            UserId = filter.UserId;
            SortBy = filter.SortBy;
        }

        public ulong GuildId { get; private set; }
        public string Name { get; private set; }
        public EAccessLevel? AccessLevel { get; private set; }
        public ulong UserId { get; private set; }
        public ERoleSort SortBy { get; private set; }

        public IEnumerable<RoleDigestModel> Items { get; set; }
        public Dictionary<byte, string> AccessLevels => new EAccessLevel[] { EAccessLevel.User, EAccessLevel.Moderator, EAccessLevel.Administrator }.ToDictionary(k => (byte)k, v => v.ToString());

        public RoleFilterModel ChangePage(int page)
        {
            return new RoleFilterModel
            {
                AccessLevel = AccessLevel,
                Name = Name,
                Page = page,
                PerPage = PerPage,
                SortBy = SortBy,
                SortDirection = SortDirection
            };
        }
    }
}
