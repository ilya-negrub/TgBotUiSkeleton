using Microsoft.Extensions.DependencyInjection;
using TgBot.Core.Extensions;
using TgBot.Core.Interfaces;
using TgBot.Core.Interfaces.Factory;
using TgBot.Core.Services.Commands;

namespace TgBot.Core.Services.Factory
{
    public class BotCommandFactory : IBotCommandFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public BotCommandFactory(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IBotCommand Crteate(IBotContext context)
        {
            var key = GetKey(context);
            return _serviceProvider.GetKeyedService<IBotCommand>(key);
        }

        public bool TryGetKey(IBotContext context, out string key)
        {
            key = GetKey(context);
            return BotCommandKey.ContainsKey(key);
        }

        private string GetKey(IBotContext context)
        {
            var update = context.Update;
            if (update.Message != null)
            {
                return update.Message.Text;
            }

            if (update.CallbackQuery != null
                && update.TryGetCallback(out var callbcak))
            {
                return callbcak;
            }


            return string.Empty;
        }
    }
}
