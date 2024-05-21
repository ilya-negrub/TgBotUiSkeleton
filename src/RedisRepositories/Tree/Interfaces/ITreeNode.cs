namespace RedisRepositories.Tree.Interfaces
{
    public interface ITreeNode
    {
        public Guid Id { get; }

        public ITreeNode Parent { get; }

        public Type Type { get; }

        public void SetParent(ITreeNode parent);
    }
}
