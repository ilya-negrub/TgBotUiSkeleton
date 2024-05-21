using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.Core.Interfaces;

namespace TgBot.Core.Services
{
    public class BotContext : IBotContext
    {
        public Guid Id { get; } = Guid.NewGuid();

        public ITelegramBotClient Client { get; }

        public User User { get; }

        public Update Update { get; }

        public BotContext(
            ITelegramBotClient client,
            User user,
            Update update)
        {
            Client = client;
            User = user;
            Update = update;
        }
    }
}
