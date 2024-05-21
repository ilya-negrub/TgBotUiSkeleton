using RedisRepositories.Hash.Interfaces;
using Telegram.Bot;
using TgBot.Core.BotMenu.NodeMenuStrategies.Interfaces;
using TgBot.Core.Interfaces;
using TgBot.Core.Redis.Repository.Entities;
using TgBot.Core.Services.Commands.Menu;

namespace TgBot.Core.BotMenu.NodeMenuStrategies.Users
{
    public class RemoveUserHandler : IYesNoQuestionHandler
    {
        private readonly IHashRepository<UserHashEntity> _userRepository;

        public RemoveUserHandler(IHashRepository<UserHashEntity> userRepository)
        {
            _userRepository = userRepository;
        }

        public string GetQuestion(CallBackStrategyPath path)
        {
            var userId = long.Parse(path.GetItemByIndex(0));

            var userInfo = _userRepository.Get(userId, x => x.UserInfo);
            var text = userInfo?.GetNameFLIU(userId);

            return $"Удалить пользователя? {Environment.NewLine}{text}";
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
                    _userRepository.Set(userId, x => x.IsAuthenticated, false);

                    await context.Client.SendTextMessageAsync(
                        msg.Chat.Id,
                        $"Пользователь успешно удален.");
                }
                else
                {
                    await context.Client.SendTextMessageAsync(
                        msg.Chat.Id,
                        $"Пользователь не найден.");
                }
            }

            return BotRenderType.PreviousMenu;
        }
    }
}
