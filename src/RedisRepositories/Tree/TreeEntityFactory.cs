using Microsoft.Extensions.DependencyInjection;
using RedisRepositories.Tree.Interfaces;

namespace RedisRepositories.Tree
{
    public class TreeEntityFactory : ITreeEntityFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public TreeEntityFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ITreeEntity Create(Type type)
        {
            return _serviceProvider.GetService(type) as ITreeEntity;
        }

        public TTreeEntity Create<TTreeEntity>() where TTreeEntity : ITreeEntity
        {
            return _serviceProvider.GetService<TTreeEntity>();
        }
    }
}
