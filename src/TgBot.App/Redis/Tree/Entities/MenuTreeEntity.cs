using Microsoft.Extensions.Logging;
using RedisRepositories.Tree.Entity;
using RedisRepositories.Tree.Interfaces;
using TgBot.App.BotMenu.Services;
using TgBot.App.BotMenu.Services.NumbersApi;
using TgBot.App.Redis.Tree.Configurations;
using TgBot.Core.BotMenu.NodeMenuStrategies.Users;
using TgBot.Core.BotMenu.Nodes;
using TgBot.Core.BotMenu.Nodes.Admin;
using TgBot.Core.BotMenu.Nodes.Settings;
using TgBot.Core.BotMenu.Nodes.Settings.User;

namespace TgBot.App.Redis.Tree.Entities
{
    public class MenuTreeEntity : BaseTreeEntity
    {
        public MenuTreeEntity(ILogger<MenuTreeEntity> logger) : base(logger)
        {
            Configuration = new MenuTreeEntityConfiguration();
        }

        public override ITreeEntityConfiguration Configuration { get; }

        protected override void OnBuildTree()
        {
            /// Redis command
            /// FLUSHDB – Deletes all keys from the connection's current database.
            /// FLUSHALL – Deletes all keys from all databases on current host.
            /// redis-cli KEYS "menu:0:*" | xargs redis-cli DEL

            var rootNode =
                Add<NodeMenuRoot>(
                () => Add<ServicesMenu>(
                    () => Add<NumbersApiMenu>(
                        () => Add<NumbersApiRandomMenu>(),
                        () => Add<NumbersApiCurrentDateMenu>())),
                () => Add<AdminMenu>(
                    () => Add<UserManagementAdminMenu>(
                        () => Add<AddUserAdminMenu<AddUserNodeMenuStrategyContext>>(),
                        () => Add<RemoveAdminMenu<RemoveUserNodeMenuStrategyContext>>(),
                        () => Add<PermissionAdminMenu>(
                            () => Add<AddUserAdminMenu<AddUserPermissionNodeMenuStrategyContext>>(),
                            () => Add<RemoveAdminMenu<RemoveUserPermissionNodeMenuStrategyContext>>()))),
                () => Add<SettingsNodeMenu>(
                    () => Add<UserSettingsNodeMenu>(
                        () => Add<UserInfoSettingsNodeMenu>(),
                        () => Add<UpdateUserInfoSettingsNodeMenu>())));
        }
    }
}
