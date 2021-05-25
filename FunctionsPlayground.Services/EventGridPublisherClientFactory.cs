using System;
using System.Collections.Concurrent;
using Azure;
using Azure.Messaging.EventGrid;

namespace FunctionsPlayground.Services
{
    // need to be registered as a singleton to allow clients to be cached
    public class EventGridPublisherClientFactory : IEventGridPublisherClientFactory
    {
        private readonly ConcurrentDictionary<string, EventGridPublisherClient> _clientCache = new ConcurrentDictionary<string, EventGridPublisherClient>();
        
        public EventGridPublisherClient Create(string topicEndpoint, string topicKey)
        {
            var client = _clientCache.GetOrAdd(topicEndpoint, _ => new EventGridPublisherClient(new Uri(topicEndpoint), new AzureKeyCredential(topicKey)));

            return client;
        }
    }
}