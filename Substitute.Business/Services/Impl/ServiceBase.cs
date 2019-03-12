using Substitute.Domain.Enums;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Substitute.Business.Services.Impl
{
    public abstract class ServiceBase
    {
        protected readonly IUserService _userService;

        public ServiceBase(IUserService userService)
        {
            _userService = userService;
        }

        protected async Task<bool> HasGuildAccessLevel(ulong userId, ulong guildId, EAccessLevel accessLevel)
        {
            EAccessLevel userAccessLevel = await _userService.GetGuildAccessLevel(userId, guildId);
            switch (accessLevel)
            {
                case EAccessLevel.Administrator:
                    return new EAccessLevel[] { EAccessLevel.Administrator, EAccessLevel.Owner }.Contains(userAccessLevel);
                case EAccessLevel.Moderator:
                    return new EAccessLevel[] { EAccessLevel.Moderator, EAccessLevel.Administrator, EAccessLevel.Owner }.Contains(userAccessLevel);
                case EAccessLevel.Owner:
                    return new EAccessLevel[] { EAccessLevel.Owner }.Contains(userAccessLevel);
                case EAccessLevel.User:
                    return new EAccessLevel[] { EAccessLevel.User, EAccessLevel.Moderator, EAccessLevel.Administrator, EAccessLevel.Owner }.Contains(userAccessLevel);
                default:
                    return false;
            }
        }
    }
}
