using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgBot.Core.Interfaces
{
    public interface IBotContext
    {
        public Guid Id { get; }

        public ITelegramBotClient Client { get; }

        public User User { get; }

        public Update Update { get; }
    }
}
