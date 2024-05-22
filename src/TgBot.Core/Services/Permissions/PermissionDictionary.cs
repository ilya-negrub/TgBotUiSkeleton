using TgBot.Core.Interfaces.Permissions;

namespace TgBot.Core.Services.Permissions
{
    public class PermissionDictionary : IPermissionDictionary
    {
        public static string Menu = nameof(Menu);
        public static string Admin = nameof(Admin);

        public IEnumerable<PermissionInfo> RegisterPermission()
        {
            yield return new PermissionInfo(Menu, "Доступ к меню");
            yield return new PermissionInfo(Admin, "Администрирование");
        }
    }
}
