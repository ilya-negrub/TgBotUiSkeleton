using TgBot.Core.BotMenu.Nodes.Interfaces;
using TgBot.Core.Interfaces;
using TgBot.Core.Services.Commands.Menu;
using TgBot.Core.Services.UserServices.Interfaces;

namespace TgBot.Core.BotMenu.Nodes.Settings.User
{
    public class UserInfoSettingsNodeMenu : BaseSettingsNodeMenu, INodeMenuHandler
    {
        private readonly IUserInfoService _userInfoService;

        public UserInfoSettingsNodeMenu(IUserInfoService userInfoService) : base("Информация пользователя")
        {
            _userInfoService = userInfoService;
        }

        public async Task<BotRenderType> Processing(IBotContext context)
        {
            await _userInfoService.SendMessageUserInfo("Информация пользователя");

            return BotRenderType.PreviousMenu;
        }
    }
}
