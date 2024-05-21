using System.Linq.Expressions;

namespace RedisRepositories.Hash.Interfaces
{
    public interface IHashRepository<TEntity>
        where TEntity : IHashEntity, new()
    {
        public bool Exists(long id);

        public bool Exists<TResult>(long id, Expression<Func<TEntity, TResult>> prop);

        public bool Exists<TResult>(long id, Expression<Func<TEntity, TResult>> prop, string fieldId);

        public TResult Get<TResult>(long id, Expression<Func<TEntity, TResult>> prop);

        public TResult Get<TResult>(long id, Expression<Func<TEntity, TResult>> prop, string fieldId);

        public void Set<TValue>(long id, Expression<Func<TEntity, TValue>> prop, TValue value);

        public void Set<TValue>(long id, Expression<Func<TEntity, TValue>> prop, string fieldId, TValue value);

        public long[] GetAll();
    }
}
