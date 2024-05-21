using RedisRepositories.Hash.Interfaces;
using TgBot.Core.Redis.Repository.Entities;

namespace TgBot.Core.BotMenu.NodeMenuStrategies.Users
{
    public class AuthUserListNodeMenuStrategy : BaseUserListNodeMenuStrategy
    {
        public AuthUserListNodeMenuStrategy(
            IHashRepository<UserHashEntity> userRepository) : base(userRepository)
        {
        }

        protected override bool WherePredicate(long userId)
        {
            return UserRepository.Get(userId, x => x.IsAuthenticated);
        }
    }
}
