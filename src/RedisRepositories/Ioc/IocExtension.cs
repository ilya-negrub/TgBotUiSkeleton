using Microsoft.Extensions.DependencyInjection;
using RedisRepositories.Hash;
using RedisRepositories.Hash.Interfaces;
using RedisRepositories.Interfaces;
using RedisRepositories.Serializer;
using RedisRepositories.Services;
using RedisRepositories.Tree;
using RedisRepositories.Tree.Interfaces;

namespace RedisRepositories.Ioc
{
    public static class IocExtension
    {
        public static void RedisRepositoriesRegister(
            this IServiceCollection services,
            IRedisConfiguration redisConfiguration,
            Action<IRepositoryConfiguration> cfgAction)
        {
            var redisProvider = new DefaultRedisProvider(redisConfiguration);
            services.RedisRepositoriesRegister(redisProvider, cfgAction);
        }

        public static void RedisRepositoriesRegister(
            this IServiceCollection services,
            IRedisProvider redisProvider,
            Action<IRepositoryConfiguration> cfgAction)
        {
            services.RegisterRedisServices(redisProvider);
            services.RegisterFactory();

            services.AddScoped<IRedisSerializer, RedisJsonSerializer>();
            services.AddScoped(typeof(IHashRepository<>), typeof(HashRepository<>));
            services.AddScoped(typeof(ITreeRepository<>), typeof(TreeRepository<>));

            cfgAction(new RepositoryConfiguration(services));
        }

        private static void RegisterFactory(this IServiceCollection services)
        {
            services.AddScoped<ITreeEntityFactory, TreeEntityFactory>();
        }

        private static void RegisterRedisServices(
            this IServiceCollection services,
            IRedisProvider redisProvider)
        {
            services.AddScoped(x => redisProvider);
            services.AddScoped(x => redisProvider.GetDatabase());
            services.AddScoped(x => redisProvider.GetServer());
        }
    }
}
