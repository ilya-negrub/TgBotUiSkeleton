namespace TgBot.Core.Interfaces
{
    public interface IBotHandler
    {
        public Task UpdateHandler(IBotContext context, CancellationToken cancellationToken);
    }
}
