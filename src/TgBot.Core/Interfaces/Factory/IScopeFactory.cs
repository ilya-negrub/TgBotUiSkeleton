namespace TgBot.Core.Interfaces.Factory
{
    internal interface IScopeFactory<T>
    {
        public void Init(Func<T> func);

        public T Create();
    }
}
