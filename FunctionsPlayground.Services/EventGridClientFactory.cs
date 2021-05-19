using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using System;

namespace FunctionsPlayground.Services
{
    public class EventGridClientFactory : IEventGridClientFactory
    {
        public IEventGridClient Create(string name)
        {
            // TODO different clients for different topics?
            string key = null;

            // the caller is responsible for disposing
            return new EventGridClient(new TopicCredentials(key));
        }
    }
}