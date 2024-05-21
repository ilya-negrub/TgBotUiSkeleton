namespace RedisRepositories.Interfaces
{
    public interface ITreeNodeFactory<TNode>
    {
        TNode CreateNode(string key);
    }
}
