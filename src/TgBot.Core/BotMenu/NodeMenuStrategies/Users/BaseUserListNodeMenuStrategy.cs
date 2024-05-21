using RedisRepositories.Hash.Interfaces;
using TgBot.Core.BotMenu.NodeMenuStrategies.Interfaces;
using TgBot.Core.Redis.Repository.Entities;

namespace TgBot.Core.BotMenu.NodeMenuStrategies.Users
{
    public abstract class BaseUserListNodeMenuStrategy : INodeMenuStrategy
    {
        private readonly IHashRepository<UserHashEntity> _userRepository;

        protected BaseUserListNodeMenuStrategy(
            IHashRepository<UserHashEntity> userRepository)
        {
            _userRepository = userRepository;
        }

        protected IHashRepository<UserHashEntity> UserRepository => _userRepository;

        public Task<INodeMenuStrategyItem[]> GetChildren(CallBackStrategyPath path)
        {
            var result = new List<INodeMenuStrategyItem>();
            var userIds = GetUserIds();

            foreach (var userId in userIds)
            {
                var userInfo = _userRepository.Get(userId, x => x.UserInfo);
                var text = userInfo?.GetNameFLIU(userId);
                result.Add(new NodeMenuStrategyItem(text, path.Concat(userId.ToString())));
            }

            return Task.FromResult(result.ToArray());
        }

        public string GetLabel(CallBackStrategyPath path)
        {
            return $"Выберите пользователя:";
        }

        protected abstract bool WherePredicate(long userId);

        private IEnumerable<long> GetUserIds()
        {
            return _userRepository
                .GetAll()
                .Where(WherePredicate);
        }
    }
}
