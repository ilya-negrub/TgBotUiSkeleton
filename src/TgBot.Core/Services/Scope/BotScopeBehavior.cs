using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.Core.Extensions;
using TgBot.Core.Interfaces.Factory;
using TgBot.Core.Interfaces.Scope;
using TgBot.Core.Redis.Identity.Interfaces;

namespace TgBot.Core.Services.Scope
{
    public class BotScopeBehavior : IAsyncDisposable
    {
        private readonly IServiceScope _serviceScope;
        private readonly IServiceProvider _provider;
        private readonly BotContext _context;

        public BotScopeBehavior(
            IServiceProvider hostProvider,
            ITelegramBotClient client,
            Update update)
        {
            _serviceScope = hostProvider.CreateScope();
            _provider = _serviceScope.ServiceProvider;
            _context = new BotContext(client, update.GetUser(), update);

            InitScopeFactory();
        }

        public async Task Processing<THandler>(CancellationToken token)
            where THandler : class, IBotScopeHandler
        {
            var userIdentity = _provider.GetRequiredService<IUserIdentity>();
            await userIdentity.Verify();

            if (userIdentity.IsAuthenticated)
            {
                var handler = _provider.GetRequiredService<THandler>();
                await handler.Processing(_context, token);
            }
        }

        public ValueTask DisposeAsync()
        {
            _serviceScope.Dispose();

            return ValueTask.CompletedTask;
        }

        public void InitScopeFactory()
        {
            var contextFactory = _provider.GetRequiredService<IScopeFactory<BotContext>>();
            contextFactory.Init(() => _context);
        }
    }
}
