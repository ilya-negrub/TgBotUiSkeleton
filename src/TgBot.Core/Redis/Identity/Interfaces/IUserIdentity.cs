namespace TgBot.Core.Redis.Identity.Interfaces
{
    public interface IUserIdentity
    {
        public long UserId { get; }

        public Guid SeesionId { get; }

        public bool IsAuthenticated { get; }

        public Task Verify();

        public bool HasPermission(Permission permission);
    }
}
