namespace TgBot.Core.Redis.Identity
{
    public enum Permission
    {
        Denied,
        Allow,
        Menu,
        Admin
    }

    public static class PermissionExtension
    {
        public static string GetFieldId(this Permission permission)
        {
            return permission.ToString().ToLower();
        }

        public static string GetName(this Permission permission)
        {
            return permission switch
            {
                Permission.Denied => "Отказано",
                Permission.Allow => "Позволять",
                Permission.Menu => "Доступ к меню",
                Permission.Admin => "Администрирование",
                _ => throw new NotImplementedException(),
            };
        }
    }
}
