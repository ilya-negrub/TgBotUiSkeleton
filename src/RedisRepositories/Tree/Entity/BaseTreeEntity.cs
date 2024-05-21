using Microsoft.Extensions.Logging;
using RedisRepositories.Tree.Interfaces;
using StackExchange.Redis;

namespace RedisRepositories.Tree.Entity
{
    public abstract class BaseTreeEntity : ITreeEntity
    {
        private readonly IList<ITreeNode> _nodes = new List<ITreeNode>();
        private readonly ILogger _logger;
        public abstract ITreeEntityConfiguration Configuration { get; }

        protected BaseTreeEntity(ILogger logger)
        {
            _logger = logger;
        }

        public void BuildTree()
        {
            _nodes.Clear();
            OnBuildTree();
        }

        protected abstract void OnBuildTree();

        public void CreateTree(IDatabase database)
        {
            foreach (var node in _nodes)
            {
                CreateHash(database, node);
                AddNodeList(database, node);
            }
        }

        protected ITreeNode Add<TNode>(params Func<ITreeNode>[] nodes)
        {
            var pNode = new TreeNode<TNode>();
            _nodes.Add(pNode);

            foreach (var func in nodes)
            {
                var cNode = func();
                cNode.SetParent(pNode);
            }

            return pNode;
        }

        private void CreateHash(IDatabase database, ITreeNode node)
        {
            string key = Configuration.GetHashKey(node.Id);

            var hashFields = new[]
            {
                new HashEntry(Configuration.KeyHashType, node.Type.FullName)
            };

            database.HashSet(key, hashFields);
            _logger.LogInformation($"CreateHash: {node.Id} {node.Type.Name}");
        }

        private void AddNodeList(IDatabase database, ITreeNode node)
        {
            var parentId = node.Parent?.Id ?? Configuration.RootNodeId;

            // пополнение списка
            var keyFullList = Configuration.GetListKey(parentId);
            database.ListRightPush(keyFullList, [Configuration.GetKeyIdFormat(node.Id)]);
            _logger.LogTrace($"Fill list: {parentId} - {node.Id}");

            // Установка родителя в списке
            var keySetParent = Configuration.GetListKey(node.Id);
            database.ListLeftPush(keySetParent, [Configuration.GetKeyIdFormat(parentId)]);
            _logger.LogTrace($"Set parant: {node.Id} - {parentId}");
        }
    }
}
