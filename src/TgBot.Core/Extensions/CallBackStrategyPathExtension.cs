using TgBot.Core.BotMenu.NodeMenuStrategies;
using TgBot.Core.Redis.Identity;

namespace TgBot.Core.Extensions
{
    public static class CallBackStrategyPathExtension
    {
        public static bool TryGetUserId(
            this CallBackStrategyPath path,
            int index,
            out long userId)
        {
            var uIdValue = path.GetItemByIndex(index);

            if (long.TryParse(uIdValue, out var userIdResult))
            {
                userId = userIdResult;
                return true;
            }

            userId = userIdResult;
            return false;
        }

        public static bool TryGetPermission(
            this CallBackStrategyPath path,
            int index,
            out Permission permission)
        {
            var value = path.GetItemByIndex(index);

            if (Enum.TryParse<Permission>(value, out var result))
            {
                permission = result;
                return true;
            }

            permission = Permission.Denied;
            return false;
        }
    }
}
