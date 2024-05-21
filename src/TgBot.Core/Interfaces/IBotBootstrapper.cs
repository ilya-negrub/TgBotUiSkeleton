namespace TgBot.Core.Interfaces
{
    public interface IBotBootstrapper
    {
        public Task Bootstrap(string[] args, CancellationToken cancellationToken = default);
    }
}
