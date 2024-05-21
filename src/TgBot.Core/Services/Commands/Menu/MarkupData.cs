using Telegram.Bot.Types.ReplyMarkups;

namespace TgBot.Core.Services.Commands.Menu
{
    public class MarkupData
    {
        public MarkupData(string text, InlineKeyboardMarkup keyboard)
        {
            Text = text;
            Keyboard = keyboard;
        }

        public string Text { get; }
        public InlineKeyboardMarkup Keyboard { get; }
    }
}
