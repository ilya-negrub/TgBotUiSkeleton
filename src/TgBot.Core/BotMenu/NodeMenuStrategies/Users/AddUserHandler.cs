using RedisRepositories.Hash.Interfaces;
using Telegram.Bot;
using TgBot.Core.BotMenu.NodeMenuStrategies.Interfaces;
using TgBot.Core.Interfaces;
using TgBot.Core.Redis.Identity;
using TgBot.Core.Redis.Repository.Entities;
using TgBot.Core.Services.Commands.Menu;
using TgBot.Core.Services.UserServices.Interfaces;

namespace TgBot.Core.BotMenu.NodeMenuStrategies.Users
{
    public class AddUserHandler : IYesNoQuestionHandler
    {
        private readonly IHashRepository<UserHashEntity> _userRepository;
        private readonly IUserInfoService _userInfoService;

        public AddUserHandler(
            IHashRepository<UserHashEntity> userRepository,
            IUserInfoService userInfoService)
        {
            _userRepository = userRepository;
            _userInfoService = userInfoService;
        }

        public string GetQuestion(CallBackStrategyPath path)
        {
            var userId = long.Parse(path.GetItemByIndex(0));

            var userInfo = _userRepository.Get(userId, x => x.UserInfo);
            var text = userInfo?.GetNameFLIU(userId);

            return $"Добавить пользователя? {Environment.NewLine}{text}";
        }

        public async Task<BotRenderType> Processing(IBotContext context, CallBackStrategyPath path)
        {
            var msg = context.Update.CallbackQuery?.Message;

            var requestValue = path.GetItemByIndex(1);

            if (requestValue == "y")
            {
                if (long.TryParse(path.GetItemByIndex(0), out var userId)
                    && _userRepository.Exists(userId))
                {
                    _userRepository.Set(userId, x => x.IsAuthenticated, true);
                    _userRepository.Set(userId, x => x.Permission, Permission.Menu.GetFieldId(), Permission.Allow);

                    await _userInfoService.SendMessageUserInfo(userId, "Пользователь успешно добавлен");

                    await _userInfoService.SendMessageToUser(
                        userId,
                        "Аутентификация Вашей учётной записи пройдено успешно.");
                }
                else
                {
                    await context.Client.SendTextMessageAsync(
                        msg.Chat.Id,
                        $"Пользователь не найден.");
                }

                return BotRenderType.MainMenu;
            }

            return BotRenderType.MainMenu;
        }
    }
}
