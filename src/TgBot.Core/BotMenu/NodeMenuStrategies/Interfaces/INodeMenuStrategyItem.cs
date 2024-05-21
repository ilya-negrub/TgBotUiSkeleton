using TgBot.Core.BotMenu.Nodes.Interfaces;

namespace TgBot.Core.BotMenu.NodeMenuStrategies.Interfaces
{
    public interface INodeMenuStrategyItem : INodeMenu
    {
        public CallBackStrategyPath Path { get; }
    }
}
