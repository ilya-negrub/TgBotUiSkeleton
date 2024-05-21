namespace TgBot.Core.Services.UserServices.Interfaces
{
    public interface IUserInfoService
    {
        public Task SendMessageUserInfo(long userId, string title);

        public Task SendMessageUserInfo(string title);

        public Task SendMessageToUser(long userId, string message);

        public Task Update();
    }
}
