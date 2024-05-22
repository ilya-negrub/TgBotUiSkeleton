using TgBot.Core.Services.Permissions;

namespace TgBot.Core.BotMenu.Nodes
{
    public class NodeMenuRoot : NodeMenu
    {
        public NodeMenuRoot() : base("Главное меню", PermissionDictionary.Menu)
        {
        }
    }
}
