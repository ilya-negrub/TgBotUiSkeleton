using Microsoft.Extensions.DependencyInjection;
using TgBot.Core.Interfaces;
using TgBot.Core.Interfaces.Permissions;
using TgBot.Core.Services.Commands;

namespace TgBot.Core.Ioc
{
    public class TgBotConfigurations
    {
        private readonly IServiceCollection _services;

        public TgBotConfigurations(IServiceCollection services)
        {
            _services = services;
        }

        public void RegisterPermission<TIPermission>()
            where TIPermission : class, IPermissionDictionary
        {
            _services.AddScoped<IPermissionDictionary, TIPermission>();
        }

        public void RegisterCommand<TCommand>(string commandKey, string permission)
            where TCommand : class, IBotCommand
        {
            BotCommandKey.RegisterPermission(commandKey, permission);
            _services.AddKeyedScoped<IBotCommand, TCommand>(commandKey);
        }
    }
}
