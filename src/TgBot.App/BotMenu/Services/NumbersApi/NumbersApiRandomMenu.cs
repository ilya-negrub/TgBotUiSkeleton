using Services.NumbersApi.Interfaces;
using TgBot.Core.Interfaces;

namespace TgBot.App.BotMenu.Services.NumbersApi
{
    public class NumbersApiRandomMenu : BaseNumbersApiMenu
    {
        public NumbersApiRandomMenu(
            IBotTask _botTask,
            INumbersApi _numbersApi)
            : base("Случайный факт", _botTask, _numbersApi)
        {
        }

        protected override Task<string> GetFact(INumbersApi numbersApi)
        {
            return numbersApi.GetRandomFact();
        }
    }
}
