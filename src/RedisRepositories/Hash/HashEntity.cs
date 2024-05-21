using RedisRepositories.Hash.Interfaces;

namespace RedisRepositories.Hash
{
    public static class HashEntity<TEntity>
        where TEntity : IHashEntity, new()
    {
        static HashEntity()
        {
            Key = GetKey();
        }

        public static string Key { get; }

        private static string GetKey()
        {
            var entity = new TEntity();
            return entity.Key;
        }
    }
}
