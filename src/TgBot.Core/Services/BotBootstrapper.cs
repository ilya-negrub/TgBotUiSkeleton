using Microsoft.Extensions.Logging;
using RedisRepositories.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.Core.Configurations;
using TgBot.Core.Extensions;
using TgBot.Core.Interfaces;
using TgBot.Core.Interfaces.Scope;
using TgBot.Core.Services.Scope;

namespace TgBot.Core.Services
{
    public class BotBootstrapper : IBotBootstrapper
    {
        private readonly ILogger<BotBootstrapper> _logger;
        private readonly IBotClientProvider _botClientProvider;
        private readonly IScopeService _scopeService;
        private readonly IDataBaseMigrationManager _dataBaseMigrationManager;
        private readonly AppConfig _appConfig;

        public BotBootstrapper(
            ILogger<BotBootstrapper> logger,
            IBotClientProvider botClientProvider,
            IScopeService scopeService,
            IDataBaseMigrationManager dataBaseMigrationManager,
            AppConfig appConfig)
        {
            _logger = logger;
            _botClientProvider = botClientProvider;
            _scopeService = scopeService;
            _dataBaseMigrationManager = dataBaseMigrationManager;
            _appConfig = appConfig;
        }

        public Task Bootstrap(string[] args, CancellationToken cancellationToken = default)
        {
            _dataBaseMigrationManager.InitTreeEntity();

            _botClientProvider.SetVersion(_appConfig.BotVersion);

            _botClientProvider
                .GetClient()
                .StartReceiving(
                    UpdateHandler,
                    ErrorHandler,
                    _botClientProvider.GetReceiverOptions(),
                    cancellationToken);


            _logger.Log(LogLevel.Information, "Bot started...");
            return Task.CompletedTask;
        }

        private Task ErrorHandler(
            ITelegramBotClient client,
            Exception exception,
            CancellationToken token)
        {
            _logger.LogError(exception, "Bot Error Handler.");

            return Task.CompletedTask;
        }

        private async Task UpdateHandler(
            ITelegramBotClient client,
            Update update,
            CancellationToken token)
        {
            try
            {
                var user = update.GetUser();
                _logger.LogInformation($"Message from {user.Id}, UpdateType = {update.Type}");

                await _scopeService.CreateScope<BotScopeHandler>(client, update, token);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Bot Error Handler.");
            }
        }
    }
}
