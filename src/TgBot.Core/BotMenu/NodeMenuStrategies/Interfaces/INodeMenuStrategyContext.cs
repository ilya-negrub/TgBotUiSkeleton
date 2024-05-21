namespace TgBot.Core.BotMenu.NodeMenuStrategies.Interfaces
{
    public interface INodeMenuStrategyContext
    {
        public INodeMenuStrategy GetStrategy(CallBackStrategyPath path);

        public bool CanProcessing(CallBackStrategyPath path);
    }
}
