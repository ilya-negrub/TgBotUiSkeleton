using RedisRepositories.Hash.Interfaces;
using TgBot.Core.Redis.Identity;

namespace TgBot.Core.Redis.Repository.Entities
{
    public class UserHashEntity : IHashEntity
    {
        public string Key { get; } = "user";

        public bool IsAuthenticated { get; set; }

        public Permission Permission { get; set; }

        public UserInfo UserInfo { get; set; }
    }

    public class UserInfo
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public long ChatId { get; set; }
    }

    public static class UserInfoExtension
    {
        public static string GetNameFL(this UserInfo userInfo, long? userId)
        {
            if (userInfo == null)
            {
                return string.Empty;
            }

            if (userId.HasValue)
            {
                return $"{userInfo.FirstName} {userInfo.LastName} ({userId})";
            }

            return $"{userInfo.FirstName} {userInfo.LastName}";
        }

        public static string GetNameFLIU(this UserInfo userInfo, long? userId)
        {
            if (userInfo == null)
            {
                return string.Empty;
            }

            if (userId.HasValue)
            {
                return $"{userInfo.FirstName} {userInfo.LastName} ({userInfo.Username}, {userId})";
            }

            return $"{userInfo.FirstName} {userInfo.LastName} ({userInfo.Username})";
        }
    }
}
