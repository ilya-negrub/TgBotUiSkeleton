using RedisRepositories.Tree.Interfaces;
namespace TgBot.App.Redis.Tree.Configurations
{
    public class MenuTreeEntityConfiguration : ITreeEntityConfiguration
    {
        private readonly Guid _rootNodeId = Guid.Empty;
        private readonly string _keyEntity = "menu";
        private readonly string _keyList = "list";
        private readonly string _keyHash = "hash";
        private readonly string _keyHashType = "type";
        private readonly string _keyInfo = "info";

        public int Version { get; } = 0;

        public Guid RootNodeId => _rootNodeId;

        public string KeyEntity => _keyEntity;

        public string KeyHashType => _keyHashType;

        public string KeyInfo => GetbaseKey(_keyInfo);

        private string GetbaseKey(params string[] keys)
        {
            return $"{_keyEntity}:{Version}:{string.Join(":", keys)}";
        }

        public string GetListKey(Guid id)
        {
            return GetbaseKey(_keyList, GetKeyIdFormat(id));
        }

        public string GetHashKey(Guid id)
        {
            return GetbaseKey(_keyHash, GetKeyIdFormat(id));
        }

        public string GetKeyIdFormat(Guid id)
        {
            return id.ToString("D");
        }
    }
}
