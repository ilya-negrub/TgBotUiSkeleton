using TgBot.Core.Services.Permissions;

namespace TgBot.Core.Services.Commands
{
    public static class BotCommandKey
    {
        private static readonly Dictionary<string, string> _keys;

        public static string Menu = "/menu";
        public static string Register = "/register";

        static BotCommandKey()
        {
            _keys = new Dictionary<string, string>
            {
                { Menu, PermissionDictionary.Menu },
            };
        }

        public static bool ContainsKey(string key)
        {
            return _keys.ContainsKey(key);
        }

        public static bool TryGetPermission(string key, out string permission)
        {
            if (_keys.TryGetValue(key, out var res))
            {
                permission = res;
                return true;
            }

            permission = string.Empty;
            return false;
        }

        internal static void RegisterPermission(string commandKey, string permission)
        {
            _keys.Add(commandKey, permission);
        }
    }
}
