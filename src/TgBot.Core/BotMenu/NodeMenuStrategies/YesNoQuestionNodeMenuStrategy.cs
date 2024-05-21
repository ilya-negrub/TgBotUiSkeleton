using TgBot.Core.BotMenu.NodeMenuStrategies.Interfaces;
using TgBot.Core.Interfaces;
using TgBot.Core.Services.Commands.Menu;

namespace TgBot.Core.BotMenu.NodeMenuStrategies
{
    public class YesNoQuestionNodeMenuStrategy<THandler> : INodeMenuStrategyHandler
            where THandler : IYesNoQuestionHandler
    {
        private readonly string[] _selectedKesy = ["y", "n"];
        private readonly THandler _handler;

        public YesNoQuestionNodeMenuStrategy(THandler handler)
        {
            _handler = handler;
        }

        public Task<INodeMenuStrategyItem[]> GetChildren(CallBackStrategyPath path)
        {
            var result = new INodeMenuStrategyItem[]
            {
                new NodeMenuStrategyItem(
                    "Да",
                    path.Concat(_selectedKesy[0])),
                new NodeMenuStrategyItem(
                    "Нет",
                    path.Concat(_selectedKesy[1]))
            };

            return Task.FromResult(result);
        }

        public string GetLabel(CallBackStrategyPath path)
        {
            return _handler.GetQuestion(path);
        }

        public Task<BotRenderType> Processing(IBotContext context, CallBackStrategyPath path)
        {
            return _handler.Processing(context, path);
        }
    }
}
