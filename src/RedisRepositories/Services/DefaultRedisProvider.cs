using RedisRepositories.Interfaces;
using StackExchange.Redis;

namespace RedisRepositories.Services
{
    public class DefaultRedisProvider : IRedisProvider
    {
        private readonly IRedisConfiguration _configuration;

        public DefaultRedisProvider(IRedisConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDatabase GetDatabase(int db = -1)
        {
            var multiplexer = GetConnection();
            return multiplexer.GetDatabase(db);
        }

        public IServer GetServer()
        {
            var multiplexer = GetConnection();
            return multiplexer.GetServer(multiplexer.GetEndPoints().FirstOrDefault());
        }

        private ConnectionMultiplexer GetConnection()
        {
            return ConnectionMultiplexer.Connect(_configuration.ConnectionString);
        }
    }
}
