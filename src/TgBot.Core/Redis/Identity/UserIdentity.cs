using RedisRepositories.Hash.Interfaces;
using TgBot.Core.Interfaces;
using TgBot.Core.Redis.Identity.Interfaces;
using TgBot.Core.Redis.Repository.Entities;
using TgBot.Core.Services.UserServices.Interfaces;

namespace TgBot.Core.Redis.Identity
{
    public class UserIdentity : IUserIdentity
    {
        private readonly IBotContext _botContext;
        private readonly IHashRepository<UserHashEntity> _userRepository;
        private readonly IRegisterUserService _registerUserService;

        public UserIdentity(
            IBotContext botContext,
            IHashRepository<UserHashEntity> userRepository,
            IRegisterUserService registerUserService)
        {
            _botContext = botContext;
            _userRepository = userRepository;
            _registerUserService = registerUserService;
            UserId = botContext.User.Id;
        }

        public long UserId { get; }

        public bool IsAuthenticated { get; private set; }

        public Guid SeesionId => _botContext.Id;

        public bool HasPermission(string permissionName)
        {
            var value = _userRepository.Get(UserId, x => x.Permission, permissionName);
            return value == PermissionValue.Allow;
        }

        public async Task Verify()
        {
            IsAuthenticated = IsUserAuthenticated();
            await ProcessingNotAuthenticatedUser();
        }

        private bool IsUserAuthenticated()
        {
            return _userRepository.Get(UserId, x => x.IsAuthenticated);
        }

        private async Task ProcessingNotAuthenticatedUser()
        {
            if (IsAuthenticated)
            {
                return;
            }

            if (_userRepository == null
                || _userRepository.GetAll().Length != 0)
            {
                await _registerUserService.Register();
                return;
            }

            await _registerUserService.CreateSuperAdmin();
        }
    }
}
