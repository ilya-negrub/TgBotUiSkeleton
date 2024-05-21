using TgBot.Core.BotMenu.NodeMenuStrategies.Interfaces;

namespace TgBot.Core.BotMenu.Nodes.Admin
{
    public class RemoveAdminMenu<TStrategyContext> : BaseAdminMenu
        where TStrategyContext : INodeMenuStrategyContext
    {
        public RemoveAdminMenu(TStrategyContext strategyContext) : base("Удалить")
        {
            StrategyContext = strategyContext;
        }

        public override INodeMenuStrategyContext StrategyContext { get; }
    }
}
