using TgBot.Core.BotMenu.NodeMenuStrategies.Interfaces;
using TgBot.Core.BotMenu.Nodes.Interfaces;
using TgBot.Core.Redis.Identity;

namespace TgBot.Core.BotMenu.Nodes.Admin
{
    public abstract class BaseAdminMenu : INodeMenu
    {
        protected BaseAdminMenu(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public Permission Permission { get; } = Permission.Admin;

        public virtual INodeMenuStrategyContext StrategyContext { get; }
    }
}
