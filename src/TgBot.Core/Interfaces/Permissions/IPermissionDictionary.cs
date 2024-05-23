using TgBot.Core.Services.Permissions;

namespace TgBot.Core.Interfaces.Permissions
{
    public interface IPermissionDictionary
    {
        public IEnumerable<PermissionInfo> RegisterPermission();
    }
}
