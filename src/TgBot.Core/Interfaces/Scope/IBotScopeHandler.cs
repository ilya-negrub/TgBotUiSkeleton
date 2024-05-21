namespace TgBot.Core.Interfaces.Scope
{
    public interface IBotScopeHandler
    {
        public Task Processing(IBotContext botContext, CancellationToken token);
    }
}
