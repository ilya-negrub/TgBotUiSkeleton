using TgBot.Core.BotMenu.NodeMenuStrategies.Interfaces;
using TgBot.Core.BotMenu.Nodes.Interfaces;
using TgBot.Core.Redis.Identity;

namespace TgBot.App.BotMenu.Services.NumbersApi
{
    public class NumbersApiMenu : INodeMenu
    {
        public string Name { get; } = "Факты о числах";

        public Permission Permission { get; } = Permission.Menu;

        public INodeMenuStrategyContext StrategyContext { get; }
    }
}
