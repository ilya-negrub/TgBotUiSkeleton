using RedisRepositories.Hash.Interfaces;
using TgBot.Core.Redis.Identity;
using TgBot.Core.Redis.Repository.Entities;

namespace TgBot.Core.BotMenu.NodeMenuStrategies.Users
{
    public class AddPermissionListNodeMenuStrategy
            : BasePermissionListNodeMenuStrategy
    {
        public AddPermissionListNodeMenuStrategy(
            IHashRepository<UserHashEntity> userRepository) : base(userRepository)
        {
        }

        protected override bool WherePredicate(long userId, Permission permission)
        {
            var permissionValue = UserRepository.Get(
                userId,
                x => x.Permission,
                permission.GetFieldId());
            return permissionValue != Permission.Allow;
        }
    }
}
