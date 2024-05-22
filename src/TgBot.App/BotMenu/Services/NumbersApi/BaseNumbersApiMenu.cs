using Services.NumbersApi.Interfaces;
using TgBot.Core.BotMenu.NodeMenuStrategies.Interfaces;
using TgBot.Core.BotMenu.Nodes.Interfaces;
using TgBot.Core.Interfaces;
using TgBot.Core.Messages.Markdown;
using TgBot.Core.Services.Commands.Menu;
using TgBot.Core.Services.Permissions;

namespace TgBot.App.BotMenu.Services.NumbersApi
{
    public abstract class BaseNumbersApiMenu(
        string _name,
        IBotTask _botTask,
        INumbersApi _numbersApi)
         : INodeMenu, INodeMenuHandler
    {
        public string Name => _name;

        public string Permission { get; } = PermissionDictionary.Menu;

        public INodeMenuStrategyContext StrategyContext { get; }

        public async Task<BotRenderType> Processing(IBotContext context)
        {
            await _botTask.Run(ProcessingInner, 120);
            return BotRenderType.PreviousMenu;
        }

        protected abstract Task<string> GetFact(INumbersApi numbersApi);

        private async Task ProcessingInner(IBotTaskMethods methods)
        {
            var factText = await GetFact(_numbersApi);

            var messageText = MarkdownBuilder
                .Create()
                .AppendLine($"{Name}:")
                .AppendLine(string.Empty)
                .Append(factText)
                .Build();

            await methods.SendStatus(messageText);
        }
    }
}
