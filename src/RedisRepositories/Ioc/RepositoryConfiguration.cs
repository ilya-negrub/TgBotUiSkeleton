using Microsoft.Extensions.DependencyInjection;
using RedisRepositories.Interfaces;
using RedisRepositories.Services;
using RedisRepositories.Tree.Interfaces;

namespace RedisRepositories.Ioc
{
    public class RepositoryConfiguration : IRepositoryConfiguration
    {
        private readonly IServiceCollection _services;

        public RepositoryConfiguration(IServiceCollection services)
        {
            _services = services;
        }

        public void RegisterTreeEntity<TTreeEntity, TNode>(params Action<RegisterTreeEntityConfiguration<TNode>>[] actions)
            where TNode : class
            where TTreeEntity : class, ITreeEntity
        {
            _services.AddScoped<TTreeEntity>();
            _services.AddScoped<ITreeNodeFactory<TNode>, TreeNodeFactory<TNode>>();
            _services.AddScoped<IDataBaseMigrationManager, DefaultDataBaseMigrationManager<TTreeEntity>>();

            var config = new RegisterTreeEntityConfiguration<TNode>(_services);
            foreach (var action in actions)
            {
                action(config);
            }
        }
    }
}
