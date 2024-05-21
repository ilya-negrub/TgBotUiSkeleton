using Newtonsoft.Json;

namespace RedisRepositories.Serializer
{
    public class RedisJsonSerializer : IRedisSerializer
    {
        public TValue Deserialize<TValue>(string value)
        {
            var data = FromBase64(value);
            return JsonConvert.DeserializeObject<TValue>(data);
        }

        public string Serialize<TValue>(TValue value)
        {
            var json = JsonConvert.SerializeObject(value);
            return ToBase64(json);
        }

        public static string ToBase64(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string FromBase64(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
