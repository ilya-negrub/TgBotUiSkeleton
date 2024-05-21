using RedisRepositories.Hash.Interfaces;
using TgBot.Core.BotMenu.NodeMenuStrategies.Interfaces;
using TgBot.Core.Extensions;
using TgBot.Core.Redis.Identity;
using TgBot.Core.Redis.Repository.Entities;

namespace TgBot.Core.BotMenu.NodeMenuStrategies.Users
{
    public abstract class BasePermissionListNodeMenuStrategy(
            IHashRepository<UserHashEntity> _userRepository)
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
                var text = permission.GetName();
                var permissionId = (int)permission;
                result.Add(new NodeMenuStrategyItem(text, path.Concat(permissionId.ToString())));
            }

            return Task.FromResult(result.ToArray());
        }

        public virtual string GetLabel(CallBackStrategyPath path)
        {
            return $"Выберите разрешение:";
        }

        protected abstract bool WherePredicate(long userId, Permission permission);

        private IEnumerable<Permission> GetPermissions(long userId)
        {
            var excluded = new[]
            {
                Permission.Denied,
                Permission.Allow
            };

            return Enum.GetValues(typeof(Permission))
                .Cast<Permission>()
                .Where(x => !excluded.Contains(x))
                .Where(x => WherePredicate(userId, x));
        }
    }
}
