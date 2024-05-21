using RedisRepositories.Hash.Interfaces;
using TgBot.Core.Redis.Identity;
using TgBot.Core.Redis.Repository.Entities;

namespace TgBot.Core.BotMenu.NodeMenuStrategies.Users
{
    public class RemovePermissionListNodeMenuStrategy
        : BasePermissionListNodeMenuStrategy
    {
        public RemovePermissionListNodeMenuStrategy(
            IHashRepository<UserHashEntity> userRepository) : base(userRepository)
        {
        }

        protected override bool WherePredicate(long userId, Permission permission)
        {
            var permissionValue = UserRepository.Get(
                userId,
                x => x.Permission,
                permission.GetFieldId());
            return permissionValue == Permission.Allow;
        }
    }
}
