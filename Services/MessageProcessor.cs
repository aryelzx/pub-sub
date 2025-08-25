using GcpMessagingDemo.Cache;
using System;
using System.Threading.Tasks;

namespace GcpMessagingDemo.Services
{
    public class MessageProcessor
    {
        private readonly RedisService _redis;

        public MessageProcessor(RedisService redis)
        {
            _redis = redis;
        }

        public async Task ProcessMessageAsync(string message)
        {
            Console.WriteLine($"Processando mensagem: {message}");

            // Salva no Redis
            await _redis.SaveAsync("last_message", message);

            var fromCache = await _redis.GetAsync("last_message");
            Console.WriteLine($"Mensagem recuperada do Redis: {fromCache}");
        }
    }
}
