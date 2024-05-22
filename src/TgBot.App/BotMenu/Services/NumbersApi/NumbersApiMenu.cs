using TgBot.Core.BotMenu.NodeMenuStrategies.Interfaces;
using TgBot.Core.BotMenu.Nodes.Interfaces;
using TgBot.Core.Services.Permissions;

namespace TgBot.App.BotMenu.Services.NumbersApi
{
    public class NumbersApiMenu : INodeMenu
    {
        public string Name { get; } = "Факты о числах";

        public string Permission { get; } = PermissionDictionary.Menu;

        public INodeMenuStrategyContext StrategyContext { get; }
    }
}
