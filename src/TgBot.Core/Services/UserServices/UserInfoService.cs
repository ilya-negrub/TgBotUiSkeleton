using RedisRepositories.Hash.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TgBot.Core.Extensions;
using TgBot.Core.Interfaces;
using TgBot.Core.Messages.Markdown;
using TgBot.Core.Redis.Repository.Entities;
using TgBot.Core.Services.UserServices.Interfaces;

namespace TgBot.Core.Services.UserServices
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IBotContext _context;
        private readonly IHashRepository<UserHashEntity> _userRepository;
        private readonly IBotTask _botTask;

        public UserInfoService(
            IBotContext context,
            IHashRepository<UserHashEntity> userRepository,
            IBotTask botTask)
        {
            _context = context;
            _userRepository = userRepository;
            _botTask = botTask;
        }

        public async Task SendMessageToUser(long userId, string message)
        {
            var userInfo = _userRepository.Get(userId, x => x.UserInfo);

            await _context.Client.SendTextMessageAsync(
                userInfo.ChatId,
                message);
        }

        public Task SendMessageUserInfo(long userId, string title)
        {
            return _botTask.Run(m =>
            {
                return SendMessageUserInfoInner(userId, title, m);
            }, 30);
        }

        public Task SendMessageUserInfo(string title)
        {
            var userId = _context.User.Id;
            return SendMessageUserInfo(userId, title);
        }

        public Task Update()
        {
            var user = _context.User;
            var msg = _context.Update.GetMessage();
            var userInfo = GetUserInfo(user, msg.Chat.Id);
            _userRepository.Set(user.Id, x => x.UserInfo, userInfo);

            return Task.CompletedTask;
        }

        private async Task SendMessageUserInfoInner(long userId, string title, IBotTaskMethods taskMethods = null)
        {
            var userInfo = _userRepository.Get(userId, x => x.UserInfo);
            var msg = _context.Update.Message
               ?? _context.Update.CallbackQuery?.Message;
            var messageText = GetTextMessage(userInfo, title);

            if (taskMethods != null)
            {
                await taskMethods.SendStatus(messageText, ParseMode.MarkdownV2);
                return;
            }

            await _context.Client.SendTextMessageAsync(
                    msg.Chat.Id,
                    messageText,
                    parseMode: ParseMode.MarkdownV2);
        }

        private string GetTextMessage(UserInfo userInfo, string title)
        {
            if (userInfo == null)
            {
                return MarkdownBuilder.Create()
                    .Append("Информация о пользователе не найдена", MarkdownStyle.Bold)
                    .Build();
            }

            return MarkdownBuilder.Create()
                .AppendLine(title, MarkdownStyle.Bold)
                .AppendProp(userInfo, "FirstName", x => x.FirstName)
                .AppendProp(userInfo, "LastName", x => x.LastName)
                .AppendProp(userInfo, "Username", x => x.Username)
                .Build();
        }

        private UserInfo GetUserInfo(User user, long chatId)
        {
            return new UserInfo
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                ChatId = chatId
            };
        }
    }
}
