using TgBot.Core.BotMenu.Nodes.Interfaces;
using TgBot.Core.Interfaces;
using TgBot.Core.Services.Commands.Menu;
using TgBot.Core.Services.UserServices.Interfaces;

namespace TgBot.Core.BotMenu.Nodes.Settings.User
{
    public class UpdateUserInfoSettingsNodeMenu : BaseSettingsNodeMenu, INodeMenuHandler
    {
        private readonly IUserInfoService _userInfoService;

        public UpdateUserInfoSettingsNodeMenu(IUserInfoService userInfoService)
            : base("Обновить информацию пользователя")
        {
            _userInfoService = userInfoService;
        }

        public async Task<BotRenderType> Processing(IBotContext context)
        {
            await _userInfoService.Update();
            await _userInfoService.SendMessageUserInfo("Информация пользователя обновлена");

            return BotRenderType.PreviousMenu;
        }
    }
}
