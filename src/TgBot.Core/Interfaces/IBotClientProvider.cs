using Telegram.Bot;
using Telegram.Bot.Polling;

namespace TgBot.Core.Interfaces
{
    public interface IBotClientProvider
    {
        public void SetVersion(long version);

        public ReceiverOptions GetReceiverOptions();

        public ITelegramBotClient GetClient();
    }
}
