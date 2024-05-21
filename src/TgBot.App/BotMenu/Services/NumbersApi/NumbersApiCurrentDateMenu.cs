using Services.NumbersApi;
using Services.NumbersApi.Interfaces;
using TgBot.Core.Interfaces;

namespace TgBot.App.BotMenu.Services.NumbersApi
{
    public class NumbersApiCurrentDateMenu : BaseNumbersApiMenu
    {
        public NumbersApiCurrentDateMenu(
            IBotTask _botTask,
            INumbersApi _numbersApi)
            : base("Факт сегодняшнего дня", _botTask, _numbersApi)
        {
        }

        protected override Task<string> GetFact(INumbersApi numbersApi)
        {
            return numbersApi.GetFact(new DateFact(DateTime.Now));
        }
    }
}
