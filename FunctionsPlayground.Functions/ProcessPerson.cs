using System;
using System.Threading.Tasks;
using FunctionsPlayground.Models;
using FunctionsPlayground.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionsPlayground.Functions
{
    public class ProcessPerson
    {
        private readonly IPersonService _personService;

        public ProcessPerson(IPersonService personService)
        {
            _personService = personService ?? throw new ArgumentNullException(nameof(personService));
        }

        [FunctionName(nameof(ProcessPerson))]
        public async Task Run(
            [EventGridTrigger]EventGridEvent eventGridEvent, 
            ILogger log)
        {
            var person = JsonConvert.DeserializeObject<Person>(eventGridEvent.Data?.ToString());

            log.LogInformation(person?.ToString());

            await _personService.Process(person);
        }
    }
}
