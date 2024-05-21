namespace RedisRepositories.Tree.Interfaces
{
    public interface ITreeEntityFactory
    {
        public ITreeEntity Create(Type type);

        public TTreeEntity Create<TTreeEntity>() where TTreeEntity : ITreeEntity;
    }
}
