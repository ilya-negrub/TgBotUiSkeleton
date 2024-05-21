using TgBot.Core.BotMenu.NodeMenuStrategies.Interfaces;

namespace TgBot.Core.BotMenu.NodeMenuStrategies
{
    public abstract class BaseNodeMenuStrategyContext : INodeMenuStrategyContext
    {
        private readonly INodeMenuStrategy[] _strategies;

        protected BaseNodeMenuStrategyContext(params INodeMenuStrategy[] satges)
        {
            _strategies = satges ?? Array.Empty<INodeMenuStrategy>();
        }

        public bool CanProcessing(CallBackStrategyPath path)
        {
            return path.Depth >= _strategies.Length;
        }

        public INodeMenuStrategy GetStrategy(CallBackStrategyPath path)
        {
            var indexStrategy = Math.Min(path.Depth, _strategies.Length - 1);
            return _strategies[indexStrategy];
        }
    }
}
