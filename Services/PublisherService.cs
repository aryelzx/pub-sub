using Google.Cloud.PubSub.V1;
using Google.Protobuf;
using Microsoft.Extensions.Options;
using GcpMessagingDemo.Config;
using System.Threading.Tasks;

namespace GcpMessagingDemo.Services
{
    public class GcpConfig
    {
        public string ProjectId { get; set; } = string.Empty;
        public string TopicId { get; set; } = string.Empty;
        public string SubscriptionId { get; set; } = string.Empty;
    }

    public class PublisherService
    {
        private readonly PublisherClient _publisher;

        public PublisherService(IOptions<GcpConfig> options)
        {
            var config = options.Value;
            var topicName = TopicName.FromProjectTopic(config.ProjectId, config.TopicId);
            _publisher = PublisherClient.Create(topicName);
        }

        public async Task PublishAsync(string message)
        {
            var msg = new PubsubMessage
            {
                Data = ByteString.CopyFromUtf8(message)
            };

            string messageId = await _publisher.PublishAsync(msg);
            Console.WriteLine($"Mensagem publicada com ID: {messageId}");
        }
    }
}
