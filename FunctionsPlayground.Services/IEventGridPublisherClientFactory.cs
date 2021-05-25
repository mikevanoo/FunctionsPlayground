using Azure.Messaging.EventGrid;

namespace FunctionsPlayground.Services
{
    public interface IEventGridPublisherClientFactory
    {
        EventGridPublisherClient Create(string topicEndpoint, string topicKey);
    }
}