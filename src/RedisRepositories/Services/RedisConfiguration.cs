using RedisRepositories.Interfaces;

namespace RedisRepositories.Services
{
    public class RedisConfiguration : IRedisConfiguration
    {
        public RedisConfiguration(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; }
    }
}
