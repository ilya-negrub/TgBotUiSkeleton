namespace TgBot.Core.BotMenu.NodeMenuStrategies.Interfaces
{
    public interface INodeMenuStrategy
    {
        public Task<INodeMenuStrategyItem[]> GetChildren(CallBackStrategyPath path);

        public string GetLabel(CallBackStrategyPath path);
    }
}
