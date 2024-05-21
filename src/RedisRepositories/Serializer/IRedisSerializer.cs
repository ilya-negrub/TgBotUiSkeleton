namespace RedisRepositories.Serializer
{
    public interface IRedisSerializer
    {
        public string Serialize<TValue>(TValue value);

        public TValue Deserialize<TValue>(string value);
    }
}
