namespace TgBot.Core.Interfaces
{
    public interface IBotTask
    {
        public Task Run(Func<IBotTaskMethods, Task> runFunc, int? timeoutRemoveMessageSec = null);
    }
}
