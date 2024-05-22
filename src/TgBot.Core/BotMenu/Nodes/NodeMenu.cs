using TgBot.Core.BotMenu.NodeMenuStrategies.Interfaces;
using TgBot.Core.BotMenu.Nodes.Interfaces;

namespace TgBot.Core.BotMenu.Nodes
{
    public abstract class NodeMenu : INodeMenu
    {
        protected NodeMenu(string name, string permission)
        {
            Name = name;
            Permission = permission;
        }

        public string Name { get; }

        public string Permission { get; }

        public INodeMenuStrategyContext StrategyContext { get; }
    }
}
