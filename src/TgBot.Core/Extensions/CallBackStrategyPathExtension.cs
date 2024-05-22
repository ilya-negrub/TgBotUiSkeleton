using System.Security;
using TgBot.Core.BotMenu.NodeMenuStrategies;
using TgBot.Core.Interfaces.Permissions;
using TgBot.Core.Redis.Identity;
using TgBot.Core.Services.Permissions;

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
    }
}
