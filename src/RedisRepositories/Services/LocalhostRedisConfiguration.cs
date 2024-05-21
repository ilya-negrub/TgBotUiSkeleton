using RedisRepositories.Interfaces;

namespace RedisRepositories.Services
{
    public class LocalhostRedisConfiguration : IRedisConfiguration
    {
        public string ConnectionString { get; } = "localhost";
    }
}
