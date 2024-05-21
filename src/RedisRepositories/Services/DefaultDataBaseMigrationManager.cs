using Microsoft.Extensions.DependencyInjection;
using RedisRepositories.Interfaces;
using RedisRepositories.Tree.Interfaces;
using StackExchange.Redis;

namespace RedisRepositories.Services
{
    public class DefaultDataBaseMigrationManager<TTreeEntity> : IDataBaseMigrationManager
        where TTreeEntity : ITreeEntity
    {
        private readonly IDatabase _database;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITreeEntityFactory _treeEntityFactory;

        public DefaultDataBaseMigrationManager(
            IServiceProvider serviceProvider,
            ITreeEntityFactory treeEntityFactory,
            IDatabase database)
        {
            _database = database;
            _serviceProvider = serviceProvider;
            _treeEntityFactory = treeEntityFactory;
        }

        public void InitTreeEntity()
        {
            var repository = _serviceProvider.GetRequiredService<ITreeRepository<TTreeEntity>>();

            var entity = _treeEntityFactory.Create<TTreeEntity>();
            if (!Built(repository))
            {
                entity.BuildTree();
                entity.CreateTree(_database);
                repository.SetTreeInfo("update", true.ToString());
            }
        }

        private bool Built<TTreeEntity>(ITreeRepository<TTreeEntity> repository)
            where TTreeEntity : ITreeEntity
        {
            var updateValue = repository.GetTreeInfo("update");
            return updateValue == true.ToString();
        }
    }
}
