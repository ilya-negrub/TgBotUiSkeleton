using TgBot.Core.Interfaces;
using TgBot.Core.Services.Commands.Menu;

namespace TgBot.Core.BotMenu.Nodes.Interfaces
{
    public interface INodeMenuHandler : INodeMenu
    {
        public Task<BotRenderType> Processing(IBotContext context);
    }
}
