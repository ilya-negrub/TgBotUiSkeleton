using TgBot.Core.BotMenu.NodeMenuStrategies.Interfaces;
using TgBot.Core.BotMenu.Nodes.Interfaces;
using TgBot.Core.Redis.Identity;

namespace TgBot.App.BotMenu.Services
{
    public class ServicesMenu : INodeMenu
    {
        public string Name { get; } = "Сервисы";

        public Permission Permission { get; } = Permission.Menu;

        public INodeMenuStrategyContext StrategyContext { get; }
    }
}
