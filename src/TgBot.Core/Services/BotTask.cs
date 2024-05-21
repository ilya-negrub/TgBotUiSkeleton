using Microsoft.Extensions.Logging;
using TgBot.Core.Interfaces;
using TgBot.Core.Services.Factory;

namespace TgBot.Core.Services
{
    public class BotTask : IBotTask
    {
        private readonly ILogger _logger;
        private readonly IFactory _factory;

        public BotTask(
            ILogger<BotTask> logger,
            IFactory factory)
        {
            _logger = logger;
            _factory = factory;
        }

        public async Task Run(Func<IBotTaskMethods, Task> runFunc, int? timeoutRemoveMessageSec = null)
        {
            var methods = _factory.Create<IBotTaskMethods>();
            var ctsLoading = SendLadingStatus(methods);

            try
            {
                //await Task.Delay(4000);
                await runFunc(methods);
                ctsLoading.Cancel();
                RemoveMessage(methods, timeoutRemoveMessageSec);
            }
            catch (Exception ex)
            {
                await methods.SendStatus($"Ощибка выполнения: {ex.Message}");
                _logger.LogError(ex, "Ощибка выполнения задачи.");
            }
        }

        private CancellationTokenSource SendLadingStatus(IBotTaskMethods methods)
        {
            var cts = new CancellationTokenSource();
            _ = Task.Factory.StartNew(async () =>
            {
                await Task.Delay(2000, cts.Token);
                if (!cts.IsCancellationRequested)
                {
                    await methods.SendStatus("Загрузка...");
                }
            });

            return cts;
        }

        private void RemoveMessage(IBotTaskMethods methods, int? timeoutSec = null)
        {
            if (!timeoutSec.HasValue || timeoutSec.Value <= 0)
            {
                return;
            }

            Task.Factory.StartNew(async () =>
            {
                await Task.Delay(timeoutSec.Value * 1000);

                if (methods.Message == null)
                {
                    return;
                }

                await methods.DeleteMessageAsync();
            });
        }

    }
}
