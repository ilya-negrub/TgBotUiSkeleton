using RedisRepositories.Hash.Interfaces;
using TgBot.Core.BotMenu.NodeMenuStrategies.Interfaces;
using TgBot.Core.Extensions;
using TgBot.Core.Interfaces.Permissions;
using TgBot.Core.Redis.Identity;
using TgBot.Core.Redis.Repository.Entities;
using TgBot.Core.Services.Permissions;

namespace TgBot.Core.BotMenu.NodeMenuStrategies.Users
{
    public abstract class BasePermissionListNodeMenuStrategy(
            IHashRepository<UserHashEntity> _userRepository,
            IPermissionManager _permissionManager)
            : INodeMenuStrategy
    {
        public IHashRepository<UserHashEntity> UserRepository => _userRepository;

        public Task<INodeMenuStrategyItem[]> GetChildren(CallBackStrategyPath path)
        {
            var result = new List<INodeMenuStrategyItem>();

            if (!path.TryGetUserId(0, out var userId))
            {
                return Task.FromResult(Array.Empty<INodeMenuStrategyItem>());
            }

            foreach (var permission in GetPermissions(userId))
            {
                var text = permission.Description;
                result.Add(new NodeMenuStrategyItem(text, path.Concat(permission.Name)));
            }

            return Task.FromResult(result.ToArray());
        }

        public virtual string GetLabel(CallBackStrategyPath path)
        {
            return $"Выберите разрешение:";
        }

        protected abstract bool WherePredicate(long userId, string permissionName);

        private IEnumerable<PermissionInfo> GetPermissions(long userId)
        {
            return _permissionManager
                .GetAll()
                .Where(x => WherePredicate(userId, x.Name));
        }
    }
}
