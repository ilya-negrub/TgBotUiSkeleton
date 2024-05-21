using TgBot.Core.BotMenu.NodeMenuStrategies.Interfaces;
using TgBot.Core.BotMenu.Nodes.Interfaces;
using TgBot.Core.Redis.Identity;

namespace TgBot.Core.BotMenu.Nodes.Settings
{
    public class BaseSettingsNodeMenu : INodeMenu
    {
        public string Name { get; }

        public Permission Permission { get; } = Permission.Menu;

        public INodeMenuStrategyContext StrategyContext { get; }

        public BaseSettingsNodeMenu(string name)
        {
            Name = name;
        }
    }
}
