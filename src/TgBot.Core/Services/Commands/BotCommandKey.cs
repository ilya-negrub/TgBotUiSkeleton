using TgBot.Core.Redis.Identity;

namespace TgBot.Core.Services.Commands
{
    public static class BotCommandKey
    {
        private static readonly Dictionary<string, Permission> _keys;

        public static string Menu = "/menu";
        public static string Register = "/register";

        static BotCommandKey()
        {
            _keys = new Dictionary<string, Permission>
            {
                { Menu, Permission.Menu },
            };
        }

        public static bool ContainsKey(string key)
        {
            return _keys.ContainsKey(key);
        }

        public static bool TryGetPermission(string key, out Permission permission)
        {
            if (_keys.TryGetValue(key, out var res))
            {
                permission = res;
                return true;
            }

            permission = Permission.Denied;
            return false;
        }
    }
}
