namespace TgBot.Core.Services.UserServices.Interfaces
{
    public interface IRegisterUserService
    {
        public Task CreateSuperAdmin();

        public Task Register();
    }
}
