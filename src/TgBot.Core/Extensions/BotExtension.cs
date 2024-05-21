using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TgBot.Core.Extensions
{
    public static class BotExtension
    {
        public static Message GetMessageOrDefault(this Update update)
        {
            return update.Type switch
            {
                UpdateType.Message => update.Message,
                UpdateType.CallbackQuery => update.CallbackQuery.Message,
                _ => null
            };
        }

        public static User GetUserOrDefault(this Update update)
        {
            return update.Type switch
            {
                UpdateType.Message => update.Message.From,
                UpdateType.CallbackQuery => update.CallbackQuery.From,
                _ => null
            };
        }

        public static User GetUser(this Update update)
        {
            return update.GetUserOrDefault()
                ?? throw new ArgumentException("Не удается получить пользователя.");
        }

        public static Message GetMessage(this Update update)
        {
            return update.GetMessageOrDefault()
                 ?? throw new ArgumentException("Не удается получить сообщение.");
        }

        public static bool TryGetCallback(this Update update, out string callback)
        {
            if (update.Type == UpdateType.CallbackQuery)
            {
                var data = update.CallbackQuery.Data;
                var items = data.Split(':');

                callback = items[0];
                return true;
            }

            callback = null;
            return false;
        }



        public static string ToBase64(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string FromBase64(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }

    public class CallbackQueryData
    {

        public string Key { get; set; }
    }

    public class CallbackQueryData<TData> : CallbackQueryData
    {
        public TData Data { get; set; }
    }
}
