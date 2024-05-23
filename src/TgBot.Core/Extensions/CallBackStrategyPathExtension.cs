using TgBot.Core.BotMenu.NodeMenuStrategies;

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
