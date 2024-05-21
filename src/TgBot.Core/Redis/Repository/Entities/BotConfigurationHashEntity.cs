using RedisRepositories.Hash.Interfaces;

namespace TgBot.Core.Redis.Repository.Entities
{
    public class BotConfigurationHashEntity : IHashEntity
    {
        public string Key => "bot.config";

        public string Token { get; set; }
    }
}
