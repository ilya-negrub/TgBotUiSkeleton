using RedisRepositories.Tree.Interfaces;

namespace RedisRepositories.Ioc
{
    public interface IRepositoryConfiguration
    {
        public void RegisterTreeEntity<TTreeEntity, TNode>(params Action<RegisterTreeEntityConfiguration<TNode>>[] actions)
            where TNode : class
            where TTreeEntity : class, ITreeEntity;
    }
}
