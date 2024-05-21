namespace Services.NumbersApi.Interfaces
{
    public interface INumbersApi
    {
        public Task<string> GetRandomFact(CategoryFact? category = null);

        public Task<string> GetFact(ITemplateFact templateFact);
    }


}
