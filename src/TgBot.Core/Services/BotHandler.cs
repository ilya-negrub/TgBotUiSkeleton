using Microsoft.Extensions.Logging;
using TgBot.Core.Interfaces;
using TgBot.Core.Interfaces.Factory;
using TgBot.Core.Redis.Identity.Interfaces;
using TgBot.Core.Services.Commands;

namespace TgBot.Core.Services
{
    public class BotHandler : IBotHandler, IDisposable
    {
        private readonly ILogger<BotHandler> _logger;
        private readonly IBotContext _context;
        private readonly IBotCommandFactory _commandFactory;
        private readonly IUserIdentity _userIdentity;

        public BotHandler(
            ILogger<BotHandler> logger,
            IBotContext context,
            IBotCommandFactory commandFactory,
            IUserIdentity userIdentity)
        {
            _logger = logger;
            _context = context;
            _commandFactory = commandFactory;
            _userIdentity = userIdentity;
        }

        public async Task UpdateHandler(IBotContext context, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"BotContextId: {_context.Id}");

            if (_commandFactory.TryGetKey(context, out var commadnKey)
                && BotCommandKey.TryGetPermission(commadnKey, out var permission)
                && _userIdentity.HasPermission(permission))
            {
                var commadn = _commandFactory.Crteate(context);
                await commadn.Update(context, cancellationToken);
                await commadn.Render(context, cancellationToken);
            }
        }

        public void Dispose()
        {
        }
    }
}
