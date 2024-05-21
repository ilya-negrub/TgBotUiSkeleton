using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.Core.Interfaces.Scope;

namespace TgBot.Core.Services.Scope
{
    public class ScopeService : IScopeService
    {
        private readonly IServiceProvider _hostProvider;

        public ScopeService(IServiceProvider hostProvider)
        {
            _hostProvider = hostProvider;
        }

        public IScopeResult<TResult> CreateScope<TResult>(Func<IServiceProvider, TResult> func)
        {
            var result = new CreateScopeResult<TResult>(_hostProvider);
            result.Processing(func);
            return result;

            /// TODO: add using or used IDisposable
            /*using*/ /*var serviceScope = _hostProvider.CreateScope();
            var provider = serviceScope.ServiceProvider;
            return func(provider);*/
        }

        public Task CreateScope<THandler>(CancellationToken token)
            where THandler : class, IScopeHandler
        {
            using var serviceScope = _hostProvider.CreateScope();
            var provider = serviceScope.ServiceProvider;
            var handler = provider.GetRequiredService<THandler>();
            return handler.Processing(token);
        }

        public async Task CreateScope<THandler>(
            ITelegramBotClient client,
            Update update,
            CancellationToken token)
            where THandler : class, IBotScopeHandler
        {
            await using var botScope = new BotScopeBehavior(_hostProvider, client, update);
            await botScope.Processing<THandler>(token);
        }
    }

    public class CreateScopeResult<TResult> : IScopeResult<TResult>
    {
        private readonly IServiceScope _serviceScope;

        public CreateScopeResult(IServiceProvider hostProvider)
        {
            _serviceScope = hostProvider.CreateScope();

        }

        public TResult Result { get; private set; }

        public void Processing(Func<IServiceProvider, TResult> func)
        {
            Result = func(_serviceScope.ServiceProvider);
        }

        public ValueTask DisposeAsync()
        {
            _serviceScope?.Dispose();
            return ValueTask.CompletedTask;
        }
    }
}
