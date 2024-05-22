using TgBot.Core.Interfaces.Permissions;

namespace TgBot.Core.Services.Permissions
{
    public class PermissionManager : IPermissionManager
    {
        private static Dictionary<string, PermissionInfo> _permissions;
        private readonly IEnumerable<IPermissionDictionary> _permissionDictionaries;

        public PermissionManager(IEnumerable<IPermissionDictionary> permissionDictionaries)
        {
            _permissionDictionaries = permissionDictionaries;
        }

        public IEnumerable<PermissionInfo> GetAll()
        {
            Init();

            return _permissions.Values;
        }

        public string GetDescription(string permissionName)
        {
            Init();

            if (_permissions.TryGetValue(permissionName, out var permission))
            {
                return permission.Description;
            }

            return string.Empty;
        }

        public bool TryGetPermission(string permissionName, out PermissionInfo permission)
        {
            Init();

            if (_permissions.TryGetValue(permissionName, out var result))
            {
                permission = result;
                return true;
            }

            permission = null;
            return false;
        }

        private void Init()
        {
            if (_permissions != null)
            {
                return;
            }

            _permissions = new Dictionary<string, PermissionInfo>();

            var models = _permissionDictionaries.SelectMany(x => x.RegisterPermission());

            foreach (var model in models)
            {
                _permissions.Add(model.Name, model);
            }
        }
    }
}
