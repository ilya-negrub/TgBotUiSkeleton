using RedisRepositories.Hash.Interfaces;
using RedisRepositories.Serializer;
using StackExchange.Redis;
using System.Linq.Expressions;

namespace RedisRepositories.Hash
{
    public class HashRepository<TEntity> : IHashRepository<TEntity>
                where TEntity : IHashEntity, new()
    {
        private readonly string _separator = ":";
        private readonly IServer _server;
        private readonly IDatabase _database;
        private readonly IRedisSerializer _serializer;

        public HashRepository(
            IServer server,
            IDatabase database,
            IRedisSerializer serializer)
        {
            _server = server;
            _database = database;
            _serializer = serializer;
        }

        public bool Exists(long id)
        {
            var key = GetEntityKey(id);
            var exists = _database.KeyExists(key);
            return exists;
        }

        public bool Exists<TResult>(long id, Expression<Func<TEntity, TResult>> prop)
        {
            var key = GetEntityKey(id);
            var keyField = GetFieldKey(prop);
            var exists = _database.HashExists(key, keyField);
            return exists;
        }

        public bool Exists<TResult>(long id, Expression<Func<TEntity, TResult>> prop, string fieldId)
        {
            var key = GetEntityKey(id);
            var keyField = GetFieldKey(prop, fieldId);
            var exists = _database.HashExists(key, keyField);
            return exists;
        }

        public TResult Get<TResult>(long id, Expression<Func<TEntity, TResult>> prop)
        {
            var key = GetEntityKey(id);
            var keyField = GetFieldKey(prop);
            return Get<TResult>(key, keyField);
        }

        public TResult Get<TResult>(long id, Expression<Func<TEntity, TResult>> prop, string fieldId)
        {
            var key = GetEntityKey(id);
            var keyField = GetFieldKey(prop, fieldId);
            return Get<TResult>(key, keyField);
        }

        public void Set<TValue>(long id, Expression<Func<TEntity, TValue>> prop, TValue value)
        {
            var key = GetEntityKey(id);
            var keyField = GetFieldKey(prop);
            Set(key, keyField, value);
        }

        public void Set<TValue>(long id, Expression<Func<TEntity, TValue>> prop, string fieldId, TValue value)
        {
            var key = GetEntityKey(id);
            var keyField = GetFieldKey(prop, fieldId);
            Set(key, keyField, value);
        }

        public long[] GetAll()
        {
            var entityKey = HashEntity<TEntity>.Key;
            var pattern = $"{entityKey}{_separator}";
            var ids = new List<long>();

            foreach (var key in _server.Keys(pattern: $"{pattern}*"))
            {
                var value = key.ToString().Replace(pattern, string.Empty);
                if (long.TryParse(value, out var id))
                {
                    ids.Add(id);
                }
            }

            return ids.ToArray();
        }

        private TResult Get<TResult>(string key, string keyField)
        {
            var value = _database.HashGet(key, keyField);

            if (!value.HasValue)
            {
                return default;
            }

            return _serializer.Deserialize<TResult>(value);
        }

        private void Set<TValue>(string key, string keyField, TValue value)
        {
            var data = _serializer.Serialize(value);
            _database.HashSet(key, keyField, data);
        }

        private string GetEntityKey(long id)
        {
            var entityKey = HashEntity<TEntity>.Key;
            return string.Join(_separator, [entityKey, id]);
        }

        private string GetFieldKey<TResult>(Expression<Func<TEntity, TResult>> prop)
        {
            var expression = (MemberExpression)prop.Body;
            return expression.Member.Name.ToLower();
        }

        private string GetFieldKey<TResult>(Expression<Func<TEntity, TResult>> prop, string id)
        {
            var keyField = GetFieldKey(prop);
            return string.Join(_separator, [keyField, id]);
        }
    }
}
