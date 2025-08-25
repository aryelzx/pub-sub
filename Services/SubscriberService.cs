using Google.Cloud.PubSub.V1;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace GcpMessagingDemo.Services
{
    public class SubscriberService
    {
        private readonly SubscriberClient _subscriber;
        private readonly MessageProcessor _processor;

        public SubscriberService(IOptions<GcpConfig> options, MessageProcessor processor)
        {
            var config = options.Value;
            var subscriptionName = SubscriptionName.FromProjectSubscription(config.ProjectId, config.SubscriptionId);
            _subscriber = SubscriberClient.Create(subscriptionName);
            _processor = processor;
        }

        public async Task StartAsync()
        {
            await _subscriber.StartAsync(async (msg, cancellationToken) =>
            {
                await _processor.ProcessMessageAsync(msg.Data.ToStringUtf8());
                return SubscriberClient.Reply.Ack;
            });
        }
    }
}
