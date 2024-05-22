using TgBot.Core.BotMenu.NodeMenuStrategies.Interfaces;
using TgBot.Core.BotMenu.Nodes.Interfaces;
using TgBot.Core.Services.Permissions;

namespace TgBot.Core.BotMenu.Nodes.Admin
{
    public abstract class BaseAdminMenu : INodeMenu
    {
        protected BaseAdminMenu(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public string Permission { get; } = PermissionDictionary.Admin;

        public virtual INodeMenuStrategyContext StrategyContext { get; }
    }
}
