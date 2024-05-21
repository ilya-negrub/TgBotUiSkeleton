namespace RedisRepositories.Tree.Interfaces
{
    public interface ITreeRepository<TTreeNode>
        where TTreeNode : ITreeEntity
    {
        public Guid GetRootId();

        public Guid[] GetChildrenById(Guid id);

        public Guid GetParentById(Guid id);

        public string GetTypeNameById(Guid id);

        public string GetTreeInfo(string key);

        public void SetTreeInfo(string key, string value);
    }
}
