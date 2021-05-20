using FunctionsPlayground.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionsPlayground.Functions
{
    public static class ProcessPerson
    {
        [FunctionName("ProcessPerson")]
        public static void Run([EventGridTrigger]EventGridEvent eventGridEvent, ILogger log)
        {
            var person = JsonConvert.DeserializeObject<Person>(eventGridEvent.Data?.ToString());

            log.LogInformation(person?.ToString());
        }
    }
}
