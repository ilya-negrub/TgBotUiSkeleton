using TgBot.Core.BotMenu.NodeMenuStrategies.Interfaces;
using TgBot.Core.BotMenu.Nodes.Interfaces;
using TgBot.Core.Services.Permissions;

namespace TgBot.App.BotMenu.Services
{
    public class ServicesMenu : INodeMenu
    {
        public string Name { get; } = "Сервисы";

        public string Permission { get; } = PermissionDictionary.Menu;

        public INodeMenuStrategyContext StrategyContext { get; }
    }
}
