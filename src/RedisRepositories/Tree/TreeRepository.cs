using RedisRepositories.Tree.Interfaces;
using StackExchange.Redis;

namespace RedisRepositories.Tree
{
    public class TreeRepository<TTreeNode> : ITreeRepository<TTreeNode>
        where TTreeNode : ITreeEntity
    {
        private readonly IDatabase _database;
        private readonly ITreeEntityFactory _treeEntityFactory;

        public TreeRepository(
            ITreeEntityFactory treeEntityFactory,
            IDatabase database)
        {
            _database = database;
            _treeEntityFactory = treeEntityFactory;
        }

        public Guid GetRootId()
        {
            var entityConfig = GetConfig();
            return GetParentById(entityConfig.RootNodeId);
        }

        public Guid[] GetChildrenById(Guid id)
        {
            var entityConfig = GetConfig();
            var key = entityConfig.GetListKey(id);
            return _database.ListRange(key, 1)
                .Select(x => ParseId(x))
                .ToArray();
        }

        public Guid GetParentById(Guid id)
        {
            var entityConfig = GetConfig();
            var key = entityConfig.GetListKey(id);
            var value = _database.ListGetByIndex(key, 0);
            return ParseId(value);
        }

        public string GetTypeNameById(Guid id)
        {
            var entityConfig = GetConfig();
            string key = entityConfig.GetHashKey(id);
            var value = _database.HashGet(key, entityConfig.KeyHashType);
            return value;
        }

        public string GetTreeInfo(string key)
        {
            var entityConfig = GetConfig();
            var value = _database.HashGet(entityConfig.KeyInfo, key);
            return value;
        }

        public void SetTreeInfo(string key, string value)
        {
            var entityConfig = GetConfig();

            if (string.IsNullOrEmpty(value))
            {
                _database.HashDelete(entityConfig.KeyInfo, key);
                return;
            }

            _database.HashSet(entityConfig.KeyInfo, [new HashEntry(key, value)]);
        }

        private ITreeEntityConfiguration GetConfig()
        {
            return _treeEntityFactory.Create<TTreeNode>().Configuration;
        }

        private Guid ParseId(string value)
        {
            return Guid.Parse(value);
        }
    }
}
