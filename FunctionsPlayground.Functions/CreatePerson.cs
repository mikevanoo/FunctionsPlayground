using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] Person person,
            ILogger log)
        {
            log.Log(LogLevel.Debug, person?.ToString());

            // validate
            var validationResult = _personService.Validate(person);
            if (!validationResult.IsValid)
            {
                return Task.FromResult<IActionResult>(validationResult.ToBadRequest("Person could not be created."));
            }

            // TODO save

            return Task.FromResult<IActionResult>(new OkObjectResult(person));
        }
    }
}
