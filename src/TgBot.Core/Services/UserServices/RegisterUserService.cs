using RedisRepositories.Hash.Interfaces;
using Telegram.Bot;
using TgBot.Core.Extensions;
using TgBot.Core.Interfaces;
using TgBot.Core.Messages.Markdown;
using TgBot.Core.Redis.Identity;
using TgBot.Core.Redis.Repository.Entities;
using TgBot.Core.Services.Commands;
using TgBot.Core.Services.Permissions;
using TgBot.Core.Services.UserServices.Interfaces;

namespace TgBot.Core.Services.UserServices
{
    public class RegisterUserService : IRegisterUserService
    {
        private readonly IBotContext _botContext;
        private readonly IHashRepository<UserHashEntity> _userRepository;
        private readonly IUserInfoService _userInfoService;

        public RegisterUserService(
            IBotContext botContext,
            IHashRepository<UserHashEntity> userRepository,
            IUserInfoService userInfoService)
        {

            _botContext = botContext;
            _userRepository = userRepository;
            _userInfoService = userInfoService;
        }

        public async Task Register()
        {
            var msg = _botContext.Update.GetMessage();

            if (msg.Text.StartsWith(BotCommandKey.Register))
            {
                await RegisterUser();
                return;
            }

            var userExists = await ExistsUserAndSendMessage();

            if (userExists)
            {
                return;
            }

            await _botContext.Client.SendTextMessageAsync(
                        msg.Chat.Id,
                        $"Необходимо пройти аутентификацию. " +
                        $"Для этого выполните команду {BotCommandKey.Register}.");
        }

        public async Task CreateSuperAdmin()
        {
            var userId = _botContext.Update.GetUser().Id;

            _userRepository.Set(userId, x => x.IsAuthenticated, true);
            SetAllowPermission(userId, PermissionDictionary.Admin);
            SetAllowPermission(userId, PermissionDictionary.Menu);
            await _userInfoService.Update();
            await _userInfoService.SendMessageUserInfo("Аккаунт инициализирован как основной");
        }

        private async Task RegisterUser()
        {
            var userExists = await ExistsUserAndSendMessage();

            if (userExists)
            {
                return;
            }

            await _userInfoService.Update();
            await _userInfoService.SendMessageUserInfo("Заявка будет рассмотрена администратором");
        }

        private async Task<bool> ExistsUserAndSendMessage()
        {
            var userId = _botContext.Update.GetUser().Id;
            var userExists = _userRepository.Exists(userId);

            if (userExists)
            {
                var msg = _botContext.Update.GetMessage();
                if (_userRepository.Get(userId, x => x.IsAuthenticated))
                {
                    await _userInfoService
                        .SendMessageUserInfo(
                            "Аутентификация Вашей учётной записи пройдено успешно");

                    return true;
                }

                var txt = MarkdownBuilder
                    .Create()
                    .Append("Заявка на регистрацию находится в статусе ")
                    .Append("рассмотрения", MarkdownStyle.Italic)
                    .Build();

                await _userInfoService
                        .SendMessageUserInfo(txt);
                return true;
            }

            return false;
        }

        private void SetAllowPermission(
           long userId,
           string permissionName)
        {
            _userRepository.Set(
               userId,
               x => x.Permission,
               permissionName,
               PermissionValue.Allow);
        }
    }
}
