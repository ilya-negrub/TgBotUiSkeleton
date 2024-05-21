using Services.NumbersApi.Interfaces;

namespace Services.NumbersApi
{
    public class NumbersApi : INumbersApi
    {
        public readonly HttpClient _client;

        public NumbersApi()
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(Constants.BaseUrl)
            };
        }

        public Task<string> GetFact(ITemplateFact templateFact)
        {
            var query = templateFact.GetQuery();
            return GetFact(query);
        }

        public Task<string> GetRandomFact(CategoryFact? category = null)
        {
            var query = !category.HasValue
                ? "random"
                : $"random/{category.Value.ForQuery()}";

            return GetFact(query);
        }

        private async Task<string> GetFact(string query)
        {
            using var responseMessage = await _client.GetAsync(query);

            if (responseMessage.IsSuccessStatusCode)
            {
                return await responseMessage.Content.ReadAsStringAsync();
            }

            return "Не удалось получить факт.";
        }
    }
}
