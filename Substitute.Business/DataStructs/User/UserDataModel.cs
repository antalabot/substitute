using Substitute.Domain.Enums;

namespace Substitute.Business.DataStructs.User
{
    public class UserDataModel
    {
        public ulong Id { get; set; }
        public string AvatarUrl { get; set; }
        public string Name { get; set; }
        public ERole Role { get; set; }
    }
}
