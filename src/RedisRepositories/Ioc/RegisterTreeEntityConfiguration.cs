using Microsoft.Extensions.DependencyInjection;

namespace RedisRepositories.Ioc
{
    public class RegisterTreeEntityConfiguration<TNode>
        where TNode : class
    {
        private readonly IServiceCollection _services;

        public RegisterTreeEntityConfiguration(IServiceCollection services)
        {
            _services = services;
        }

        public void RegisterTreeNode<TNodeImp>(params Action<RegisterTreeNodeConfiguration>[] actions)
            where TNodeImp : class, TNode
        {
            var type = typeof(TNodeImp);
            var kye = type.FullName;

            _services.AddKeyedScoped<TNode, TNodeImp>(kye);

            var config = new RegisterTreeNodeConfiguration(_services);
            foreach (var action in actions)
            {
                action(config);
            }
        }
    }
}
