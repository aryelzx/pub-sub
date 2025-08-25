using GcpMessagingDemo.Config;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace GcpMessagingDemo.Cache
{
    public class RedisService
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _db;

        public RedisService(IOptions<RedisConfig> options)
        {
            var config = options.Value;
            _redis = ConnectionMultiplexer.Connect($"{config.Host}:{config.Port}");
            _db = _redis.GetDatabase();
        }

        public async Task SaveAsync(string key, string value)
        {
            await _db.StringSetAsync(key, value);
        }

        public async Task<string?> GetAsync(string key)
        {
            return await _db.StringGetAsync(key);
        }
    }
}
