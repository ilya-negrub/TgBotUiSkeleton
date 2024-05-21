using Services.NumbersApi.Interfaces;

namespace Services.NumbersApi
{
    public abstract class TemplateFact(int _number, CategoryFact _category)
        : ITemplateFact
    {
        public string GetQuery()
        {
            return $"{_number}/{_category.ForQuery()}";
        }
    }
}
