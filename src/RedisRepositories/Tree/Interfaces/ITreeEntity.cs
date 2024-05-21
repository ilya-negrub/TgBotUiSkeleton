using StackExchange.Redis;

namespace RedisRepositories.Tree.Interfaces
{
    public interface ITreeEntity
    {
        public ITreeEntityConfiguration Configuration { get; }

        public void BuildTree();

        public void CreateTree(IDatabase database);
    }
}
