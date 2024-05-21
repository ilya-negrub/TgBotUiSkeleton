﻿using TgBot.Core.Interfaces;
using TgBot.Core.Services.Commands.Menu;

namespace TgBot.Core.BotMenu.NodeMenuStrategies.Interfaces
{
    public interface IYesNoQuestionHandler
    {
        public Task<BotRenderType> Processing(IBotContext context, CallBackStrategyPath path);

        public string GetQuestion(CallBackStrategyPath path);
    }
}
