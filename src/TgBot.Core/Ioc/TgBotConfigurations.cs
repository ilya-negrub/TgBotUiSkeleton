using Microsoft.Extensions.DependencyInjection;
using TgBot.Core.Interfaces;
using TgBot.Core.Interfaces.Permissions;

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

        public void RegisterCommand<TCommand>(string commandKey)
            where TCommand : class, IBotCommand
        {
            _services.AddKeyedScoped<IBotCommand, TCommand>(commandKey);
        }
    }
}
