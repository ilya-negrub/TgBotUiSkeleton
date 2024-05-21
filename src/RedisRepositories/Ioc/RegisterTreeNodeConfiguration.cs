using Microsoft.Extensions.DependencyInjection;

namespace RedisRepositories.Ioc
{
    public class RegisterTreeNodeConfiguration
    {
        private readonly IServiceCollection _services;

        public RegisterTreeNodeConfiguration(IServiceCollection services)
        {
            _services = services;
        }

        public void Register<TNode>()
            where TNode : class
        {
            _services.AddScoped<TNode>();
        }
    }
}
