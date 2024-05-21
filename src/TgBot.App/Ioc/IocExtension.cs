using Microsoft.Extensions.DependencyInjection;
using RedisRepositories.Services;
using Services.NumbersApi.Ioc;
using TgBot.App.BotMenu.Services;
using TgBot.App.BotMenu.Services.NumbersApi;
using TgBot.App.Redis.Tree.Entities;
using TgBot.Core.BotMenu.NodeMenuStrategies;
using TgBot.Core.BotMenu.NodeMenuStrategies.Users;
using TgBot.Core.BotMenu.Nodes;
using TgBot.Core.BotMenu.Nodes.Admin;
using TgBot.Core.BotMenu.Nodes.Settings;
using TgBot.Core.BotMenu.Nodes.Settings.User;
using TgBot.Core.Configurations;
using TgBot.Core.Ioc;

namespace TgBot.App.Ioc
{
    internal static class IocExtension
    {
        private static readonly AppConfig _appConfig = new AppConfig();

        public static void Register(this IServiceCollection services)
        {
            services.AddSingleton(_appConfig);

            services.RegisterTgBot();

            services.RegisterMenuTree();

            services.RegisterNumbersApi();
        }

        private static void RegisterMenuTree(this IServiceCollection services)
        {
            services.RegisterMenuTree<MenuTreeEntity>(
                new RedisConfiguration(_appConfig.RedisConnectionString),
                rcfg => rcfg.RegisterTreeNode<NodeMenuRoot>(),
                    // Services
                    cfg => cfg.RegisterTreeNode<ServicesMenu>(),
                    cfg => cfg.RegisterTreeNode<NumbersApiMenu>(),
                    cfg => cfg.RegisterTreeNode<NumbersApiRandomMenu>(),
                    cfg => cfg.RegisterTreeNode<NumbersApiCurrentDateMenu>(),
                    // Basic
                    cfg => cfg.RegisterTreeNode<AdminMenu>(),
                    cfg => cfg.RegisterTreeNode<UserManagementAdminMenu>(),
                    // Add User
                    cfg => cfg.RegisterTreeNode<AddUserAdminMenu<AddUserNodeMenuStrategyContext>>(
                        x => x.Register<AddUserNodeMenuStrategyContext>(),
                        x => x.Register<AddUserListNodeMenuStrategy>(),
                        x => x.Register<AuthUserListNodeMenuStrategy>(),
                        x => x.Register<AddUserHandler>(),
                        x => x.Register<YesNoQuestionNodeMenuStrategy<AddUserHandler>>()),
                    // Remove User
                    cfg => cfg.RegisterTreeNode<RemoveAdminMenu<RemoveUserNodeMenuStrategyContext>>(
                        x => x.Register<RemoveUserNodeMenuStrategyContext>(),
                        x => x.Register<RemoveUserHandler>(),
                        x => x.Register<YesNoQuestionNodeMenuStrategy<RemoveUserHandler>>()),
                    // Settings
                    cfg => cfg.RegisterTreeNode<SettingsNodeMenu>(),
                    cfg => cfg.RegisterTreeNode<UserSettingsNodeMenu>(),
                    cfg => cfg.RegisterTreeNode<UserInfoSettingsNodeMenu>(),
                    cfg => cfg.RegisterTreeNode<UpdateUserInfoSettingsNodeMenu>(),
                    // Permission
                    cfg => cfg.RegisterTreeNode<PermissionAdminMenu>(),
                    // Permission Add
                    cfg => cfg.RegisterTreeNode<AddUserAdminMenu<AddUserPermissionNodeMenuStrategyContext>>(
                        x => x.Register<AddUserPermissionNodeMenuStrategyContext>(),
                        x => x.Register<AddPermissionListNodeMenuStrategy>(),
                        x => x.Register<AddUserPermissionHandler>(),
                        x => x.Register<YesNoQuestionNodeMenuStrategy<AddUserPermissionHandler>>()),
                    // Permission Remove
                    cfg => cfg.RegisterTreeNode<RemoveAdminMenu<RemoveUserPermissionNodeMenuStrategyContext>>(
                        x => x.Register<RemoveUserPermissionNodeMenuStrategyContext>(),
                        x => x.Register<RemovePermissionListNodeMenuStrategy>(),
                        x => x.Register<RemoveUserPermissionHandler>(),
                        x => x.Register<YesNoQuestionNodeMenuStrategy<RemoveUserPermissionHandler>>())
                    );
        }
    }
}
