using RedisRepositories.Hash.Interfaces;
using TgBot.Core.Interfaces.Permissions;
using TgBot.Core.Redis.Identity;
using TgBot.Core.Redis.Repository.Entities;

namespace TgBot.Core.BotMenu.NodeMenuStrategies.Users
{
    public class RemovePermissionListNodeMenuStrategy
        : BasePermissionListNodeMenuStrategy
    {
        public RemovePermissionListNodeMenuStrategy(
            IHashRepository<UserHashEntity> userRepository,
            IPermissionManager permissionManager)
            : base(userRepository, permissionManager)
        {
        }

        protected override bool WherePredicate(long userId, string permissionName)
        {
            var permissionValue = UserRepository.Get(
                userId,
                x => x.Permission,
                permissionName);
            return permissionValue == PermissionValue.Allow;
        }
    }
}
