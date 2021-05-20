using System;
using System.Threading.Tasks;
using FunctionsPlayground.Models;
using FunctionsPlayground.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace FunctionsPlayground.Functions
{
    public class CreatePerson
    {
        private readonly IPersonService _personService;

        public CreatePerson(IPersonService personService)
        {
            _personService = personService ?? throw new ArgumentNullException(nameof(personService));
        }

        [FunctionName(nameof(CreatePerson))]
        public async Task<ActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] PersonRequest person,
            ILogger log)
        {
            log.LogInformation(person?.ToString());

            // validate
            var validationResult = _personService.Validate(person);
            if (!validationResult.IsValid)
            {
                return validationResult.ToBadRequest("Person could not be created.");
            }

            // save
            var savedPerson = await _personService.Save(person);

            return new OkObjectResult(savedPerson);
        }
    }
}
