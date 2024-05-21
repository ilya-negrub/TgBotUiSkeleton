using TgBot.Core.BotMenu.NodeMenuStrategies.Interfaces;
using TgBot.Core.Redis.Identity;

namespace TgBot.Core.BotMenu.NodeMenuStrategies
{
    public class NodeMenuStrategyItem : INodeMenuStrategyItem
    {
        public NodeMenuStrategyItem(string name, CallBackStrategyPath path)
        {
            Name = name;
            Path = path;
        }

        public CallBackStrategyPath Path { get; }

        public string Name { get; }

        public Permission Permission { get; } = Permission.Admin;

        public INodeMenuStrategyContext StrategyContext { get; }
    }
}
