using RedisRepositories.Tree.Interfaces;

namespace RedisRepositories.Tree
{
    public class TreeNode<TNode> : ITreeNode
    {
        public Guid Id { get; } = Guid.NewGuid();

        public ITreeNode Parent { get; private set; }

        public Type Type => typeof(TNode);

        public void SetParent(ITreeNode parent)
        {
            Parent = parent;
        }
    }
}
