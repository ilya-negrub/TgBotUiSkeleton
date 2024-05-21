using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgBot.Core.Interfaces.Scope
{
    public interface IScopeService
    {
        public IScopeResult<TResult> CreateScope<TResult>(Func<IServiceProvider, TResult> func);

        public Task CreateScope<THandler>(CancellationToken token)
            where THandler : class, IScopeHandler;

        public Task CreateScope<THandler>(
            ITelegramBotClient client,
            Update update,
            CancellationToken token)
            where THandler : class, IBotScopeHandler;
    }
}
