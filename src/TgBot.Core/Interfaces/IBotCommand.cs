namespace TgBot.Core.Interfaces
{
    /// <summary>
    /// Команды, регистрируются по ключу.
    /// </summary>
    public interface IBotCommand
    {
        public Task Update(IBotContext context, CancellationToken cancellationToken);

        public Task Render(IBotContext context, CancellationToken cancellationToken);
    }
}
