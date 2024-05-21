using RedisRepositories.Hash.Interfaces;
using TgBot.Core.Redis.Repository.Entities;

namespace TgBot.Core.BotMenu.NodeMenuStrategies.Users
{
    public class AddUserListNodeMenuStrategy : BaseUserListNodeMenuStrategy
    {
        public AddUserListNodeMenuStrategy(
            IHashRepository<UserHashEntity> userRepository)
            : base(userRepository)
        {
        }

        protected override bool WherePredicate(long userId)
        {
            return !UserRepository.Get(userId, x => x.IsAuthenticated);
        }
    }
}
