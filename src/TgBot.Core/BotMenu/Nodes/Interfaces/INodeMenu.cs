using TgBot.Core.BotMenu.NodeMenuStrategies.Interfaces;
using TgBot.Core.Redis.Identity;

namespace TgBot.Core.BotMenu.Nodes.Interfaces
{
    public interface INodeMenu
    {
        // TODO: Add propery Key, для поддержки рефакторинга типа.
        public string Name { get; }

        public Permission Permission { get; }

        public INodeMenuStrategyContext StrategyContext { get; }
    }
}
