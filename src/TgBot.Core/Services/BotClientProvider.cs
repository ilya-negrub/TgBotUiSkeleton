using RedisRepositories.Hash.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Polling;
using TgBot.Core.Interfaces;
using TgBot.Core.Redis.Repository.Entities;

namespace TgBot.Core.Services
{
    public class BotClientProvider(
        IHashRepository<BotConfigurationHashEntity> _configRepository)
        : IBotClientProvider
    {
        private long _version = 0;
        private TelegramBotClient _client;

        public ITelegramBotClient GetClient()
        {
            if (_client == null)
            {
                var token = GetToken();
                _client = new TelegramBotClient(token);
            }

            return _client;
        }

        public ReceiverOptions GetReceiverOptions()
        {
            return new ReceiverOptions()
            {
                AllowedUpdates = { }
            };
        }

        public void SetVersion(long version)
        {
            _version = version;
        }

        private string GetToken()
        {
            var token = _configRepository.Get(_version, x => x.Token);
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException("Bot token not found.");
            }

            return token;
        }
    }
}
