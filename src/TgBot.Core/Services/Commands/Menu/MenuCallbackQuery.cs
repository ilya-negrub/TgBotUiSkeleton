namespace TgBot.Core.Services.Commands.Menu
{
    public class MenuCallbackQuery
    {
        private static readonly char _separator = ':';

        public MenuCallbackQuery(Guid menuId, string satgePath)
        {
            MenuId = menuId;
            SatgePath = satgePath;
        }

        public string Key { get; } = BotCommandKey.Menu;

        public Guid MenuId { get; }

        public string SatgePath { get; }

        public static bool TryParse(string callbackData, out MenuCallbackQuery callbackQuery)
        {
            if (string.IsNullOrEmpty(callbackData))
            {
                callbackQuery = null;
                return false;
            }

            var items = callbackData.Split(_separator);
            var satgePath = items.Length > 2 ? items[2] : string.Empty;

            if (items.Length > 1 && Guid.TryParse(items[1], out var menuId))
            {
                callbackQuery = new MenuCallbackQuery(menuId, satgePath);
                return true;
            }

            callbackQuery = null;
            return false;
        }

        public string GetData()
        {
            return string.Join(_separator, [Key, MenuId, SatgePath]);
        }
    }
}
