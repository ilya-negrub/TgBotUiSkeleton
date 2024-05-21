using TgBot.Core.BotMenu.NodeMenuStrategies;
using TgBot.Core.BotMenu.Nodes.Interfaces;

namespace TgBot.Core.Services.Commands.Menu
{
    public class BotMenuContext
    {
        public Guid RootId { get; init; }

        public MenuCallbackQuery CallBack { get; init; }

        public CallBackStrategyPath CallBackStrategyPath { get; init; }

        public Guid SelectedMenuId { get; init; }

        public Guid ParentId { get; init; }

        public Guid[] ChildrenIds { get; init; }

        public INodeMenu SelectedNode { get; init; }
    }
}
