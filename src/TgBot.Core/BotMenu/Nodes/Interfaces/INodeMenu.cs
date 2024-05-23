using TgBot.Core.BotMenu.NodeMenuStrategies.Interfaces;

namespace TgBot.Core.BotMenu.Nodes.Interfaces
{
    public interface INodeMenu
    {
        // TODO: Add propery Key, для поддержки рефакторинга типа.
        public string Name { get; }

        public string Permission { get; }

        public INodeMenuStrategyContext StrategyContext { get; }
    }
}
