using TgBot.Core.Interfaces;
using TgBot.Core.Interfaces.Scope;
using TgBot.Core.Redis.Identity.Interfaces;

namespace TgBot.Core.Services.Scope
{
    public class BotScopeHandler : IBotScopeHandler
    {
        private readonly IUserIdentity _userIdentity;
        private readonly IBotHandler _botHandler;

        public BotScopeHandler(
            IUserIdentity userIdentity,
            IBotHandler botHandler)
        {
            _userIdentity = userIdentity;
            _botHandler = botHandler;
        }

        public async Task Processing(IBotContext botContext, CancellationToken token)
        {
            await _userIdentity.Verify();

            if (_userIdentity.IsAuthenticated)
            {
                await _botHandler.UpdateHandler(botContext, token);
            }
        }
    }
}
