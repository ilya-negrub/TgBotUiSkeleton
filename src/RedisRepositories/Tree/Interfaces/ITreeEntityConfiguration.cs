namespace RedisRepositories.Tree.Interfaces
{
    public interface ITreeEntityConfiguration
    {
        public int Version { get; }

        public Guid RootNodeId { get; }

        public string KeyEntity { get; }

        public string KeyHashType { get; }

        public string KeyInfo { get; }

        public string GetListKey(Guid id);

        public string GetHashKey(Guid id);

        public string GetKeyIdFormat(Guid id);
    }
}
