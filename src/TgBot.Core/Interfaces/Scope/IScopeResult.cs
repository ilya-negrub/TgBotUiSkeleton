namespace TgBot.Core.Interfaces.Scope
{
    public interface IScopeResult<TResult> : IAsyncDisposable
    {
        public TResult Result { get; }
    }
}