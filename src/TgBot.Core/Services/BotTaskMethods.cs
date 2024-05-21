using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TgBot.Core.Extensions;
using TgBot.Core.Interfaces;

namespace TgBot.Core.Services
{
    public class BotTaskMethods : IBotTaskMethods
    {
        private readonly IBotContext _botContext;
        private Message _message;

        public BotTaskMethods(IBotContext botContext)
        {
            _botContext = botContext;
        }

        public Message Message => _message;

        public async Task SendStatus(string text, ParseMode? parseMode = null)
        {
            var msg = _botContext.Update.GetMessage();

            if (_message == null)
            {
                _message = await _botContext.Client.SendTextMessageAsync(
                    msg.Chat.Id,
                    text,
                    parseMode: parseMode);
            }
            else
            {
                _message = await _botContext.Client.EditMessageTextAsync(
                    _message.Chat.Id,
                    _message.MessageId,
                    text,
                    parseMode: parseMode);
            }
        }

        public async Task DeleteMessageAsync()
        {
            if (_message == null)
            {
                return;
            }

            await _botContext.Client.DeleteMessageAsync(
                _message.Chat.Id,
                _message.MessageId);
        }
    }
}
