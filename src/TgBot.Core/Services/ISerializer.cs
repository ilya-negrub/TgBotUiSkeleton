using Newtonsoft.Json;

namespace TgBot.Core.Services
{
    public interface ISerializer
    {
        public string Serialize<TValue>(TValue value);

        public TValue Deserialize<TValue>(string value);
    }

    public class JsonSerializer : ISerializer
    {
        public TValue Deserialize<TValue>(string value)
        {
            return JsonConvert.DeserializeObject<TValue>(value);
        }

        public string Serialize<TValue>(TValue value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}
