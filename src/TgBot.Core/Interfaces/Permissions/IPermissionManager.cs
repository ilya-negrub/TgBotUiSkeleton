using TgBot.Core.Services.Permissions;

namespace TgBot.Core.Interfaces.Permissions
{
    public interface IPermissionManager
    {
        public IEnumerable<PermissionInfo> GetAll();

        public bool TryGetPermission(string permissionName, out PermissionInfo permission);

        public string GetDescription(string permissionName);
    }
}
