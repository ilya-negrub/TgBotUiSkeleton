using Microsoft.Extensions.DependencyInjection;
using RedisRepositories.Interfaces;

namespace RedisRepositories.Services
{
    public class TreeNodeFactory<TNode> : ITreeNodeFactory<TNode>
    {
        private readonly IServiceProvider _serviceProvider;

        public TreeNodeFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TNode CreateNode(string key)
        {
            return _serviceProvider.GetKeyedService<TNode>(key);
        }
    }
}
