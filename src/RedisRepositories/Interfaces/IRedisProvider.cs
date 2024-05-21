using StackExchange.Redis;

namespace RedisRepositories.Interfaces
{
    public interface IRedisProvider
    {
        public IDatabase GetDatabase(int db = -1);

        public IServer GetServer();
    }
}
