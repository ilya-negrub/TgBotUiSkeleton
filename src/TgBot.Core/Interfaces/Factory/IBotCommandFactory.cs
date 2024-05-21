namespace TgBot.Core.Interfaces.Factory
{
    public interface IBotCommandFactory
    {
        public IBotCommand Crteate(IBotContext context);

        public bool TryGetKey(IBotContext context, out string key);
    }
}
