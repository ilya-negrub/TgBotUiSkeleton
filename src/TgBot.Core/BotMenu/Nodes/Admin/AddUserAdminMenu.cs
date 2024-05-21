using TgBot.Core.BotMenu.NodeMenuStrategies.Interfaces;

namespace TgBot.Core.BotMenu.Nodes.Admin
{
    public class AddUserAdminMenu<TStrategyContext> : BaseAdminMenu
        where TStrategyContext : INodeMenuStrategyContext
    {
        public AddUserAdminMenu(TStrategyContext StrategyContext) : base("Добавить")
        {
            this.StrategyContext = StrategyContext;
        }

        public override INodeMenuStrategyContext StrategyContext { get; }
    }
}
