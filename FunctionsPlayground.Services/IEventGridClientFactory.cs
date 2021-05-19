using Microsoft.Azure.EventGrid;

namespace FunctionsPlayground.Services
{
    public interface IEventGridClientFactory
    {
        IEventGridClient Create(string name);
    }
}