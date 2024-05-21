using TgBot.Core.BotMenu.NodeMenuStrategies.Interfaces;
using TgBot.Core.BotMenu.Nodes.Interfaces;
using TgBot.Core.Redis.Identity;

namespace TgBot.Core.BotMenu.Nodes
{
    public abstract class NodeMenu : INodeMenu
    {
        protected NodeMenu(string name, Permission permission)
        {
            Name = name;
            Permission = permission;
        }

        public string Name { get; }

        public Permission Permission { get; }

        public INodeMenuStrategyContext StrategyContext { get; }
    }
}
