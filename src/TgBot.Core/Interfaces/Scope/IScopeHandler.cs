namespace TgBot.Core.Interfaces.Scope
{
    public interface IScopeHandler
    {
        public Task Processing(CancellationToken token);
    }
}
