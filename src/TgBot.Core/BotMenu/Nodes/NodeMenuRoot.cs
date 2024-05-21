using TgBot.Core.Redis.Identity;

namespace TgBot.Core.BotMenu.Nodes
{
    public class NodeMenuRoot : NodeMenu
    {
        public NodeMenuRoot() : base("Главное меню", Permission.Menu)
        {
        }
    }
}
