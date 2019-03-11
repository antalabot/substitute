using Substitute.Business.DataStructs.User;
using Substitute.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Substitute.Business.Services
{
    public interface IUserService
    {
        EAccessLevel GetGuildAccessLevel(ulong userId, ulong guildId);
        UserDataModel GetUserData(string token);
    }
}
