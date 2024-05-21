namespace TgBot.Core.Configurations
{
    public class AppConfig
    {
        /// <summary>
        /// Строка подключения Redis.
        /// </summary>
        public string RedisConnectionString { get; set; } = "localhost";

        /// <summary>
        /// Версия конфигурации бота.
        /// </summary>
        public long BotVersion { get; set; } = 0;
    }
}
