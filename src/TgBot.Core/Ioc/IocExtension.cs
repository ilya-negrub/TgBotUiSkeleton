﻿using Microsoft.Extensions.DependencyInjection;
using RedisRepositories.Interfaces;
using RedisRepositories.Ioc;
using RedisRepositories.Tree.Interfaces;
using TgBot.Core.BotMenu.Nodes.Interfaces;
using TgBot.Core.Interfaces;
using TgBot.Core.Interfaces.Factory;
using TgBot.Core.Interfaces.Permissions;
using TgBot.Core.Interfaces.Scope;
using TgBot.Core.Redis.Identity;
using TgBot.Core.Redis.Identity.Interfaces;
using TgBot.Core.Services;
using TgBot.Core.Services.Commands;
using TgBot.Core.Services.Commands.Menu;
using TgBot.Core.Services.Factory;
using TgBot.Core.Services.Permissions;
using TgBot.Core.Services.Scope;
using TgBot.Core.Services.UserServices;
using TgBot.Core.Services.UserServices.Interfaces;

namespace TgBot.Core.Ioc
{
    public static class IocExtension
    {
        public static void RegisterTgBot(
            this IServiceCollection services,
            Action<TgBotConfigurations> actionConfig = null)
        {
            var config = new TgBotConfigurations(services);

            services.RegisterScopeService();
            services.RegistreFactory();

            services.AddSingleton<ISerializer, JsonSerializer>();
            services.AddScoped<IBotClientProvider, BotClientProvider>();
            services.AddScoped<IBotBootstrapper, BotBootstrapper>();
            services.AddScoped<IBotHandler, BotHandler>();

            services.AddScoped<IUserIdentity, UserIdentity>();
            services.AddScoped<IRegisterUserService, RegisterUserService>();
            services.RegisterBotContext<BotContext>();

            // Permission
            services.AddScoped<IPermissionDictionary, PermissionDictionary>();
            services.AddScoped<IPermissionManager, PermissionManager>();

            services.RegistreBotTask();

            // Services
            services.AddScoped<IUserInfoService, UserInfoService>();

            actionConfig?.Invoke(config);
        }

        public static void RegisterMenuTree<TTreeEntity>(
            this IServiceCollection services,
            IRedisConfiguration redisConfiguration,
            params Action<RegisterTreeEntityConfiguration<INodeMenu>>[] actions)
            where TTreeEntity : class, ITreeEntity
        {
            services.RedisRepositoriesRegister(
                redisConfiguration,
                rcfg => rcfg.RegisterTreeEntity<TTreeEntity, INodeMenu>(actions));

            services.AddKeyedScoped<IBotCommand, BotMenuCommand<TTreeEntity>>(BotCommandKey.Menu);
        }

        public static IScopeResult<IBotBootstrapper> GetBootstrapper(this IServiceProvider provider)
        {
            var scope = provider.GetService<IScopeService>();
            return scope.CreateScope(x => x.GetService<IBotBootstrapper>());
        }

        private static void RegisterScopeService(this IServiceCollection services)
        {
            services.AddSingleton<IScopeService, ScopeService>();
            services.AddScoped<BotScopeHandler>();
        }

        private static void RegistreFactory(this IServiceCollection services)
        {
            services.AddScoped<IFactory, Factory>();
            services.AddScoped<IBotCommandFactory>(x => new BotCommandFactory(x));
        }

        private static void RegistreBotTask(this IServiceCollection services)
        {
            services.AddScoped<IBotTask, BotTask>();
            services.AddScoped<IBotTaskMethods, BotTaskMethods>();
        }

        private static void RegisterBotContext<TContext>(this IServiceCollection services)
            where TContext : IBotContext
        {
            services.AddScoped<IScopeFactory<TContext>>(x => new ScopeFactory<TContext>());
            services.AddScoped<IBotContext>(x => x.GetRequiredService<IScopeFactory<TContext>>().Create());
        }
    }
}
