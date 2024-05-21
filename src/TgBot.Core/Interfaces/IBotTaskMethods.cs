using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TgBot.Core.Interfaces
{
    public interface IBotTaskMethods
    {
        public Message Message { get; }

        public Task SendStatus(string text, ParseMode? parseMode = null);

        public Task DeleteMessageAsync();
    }
}
