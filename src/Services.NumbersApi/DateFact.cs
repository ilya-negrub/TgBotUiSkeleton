using Services.NumbersApi.Interfaces;

namespace Services.NumbersApi
{
    public class DateFact(DateTime _date) : ITemplateFact
    {
        public string GetQuery()
        {
            return $"{_date.Month}/{_date.Day}/{CategoryFact.Date.ForQuery()}";
        }
    }
}
