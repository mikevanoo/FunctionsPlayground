using System;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using FunctionsPlayground.Models;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;

namespace FunctionsPlayground.Services
{
    public class PersonService : IPersonService
    {
        private readonly IValidator<PersonRequest> _validator;
        private readonly IEventGridClientFactory _eventGridClientFactory;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _autoMapper;

        public PersonService(
            IValidator<PersonRequest> validator,
            IEventGridClientFactory eventGridClientFactory,
            IPersonRepository personRepository,
            IMapper autoMapper)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _eventGridClientFactory = eventGridClientFactory ?? throw new ArgumentNullException(nameof(eventGridClientFactory));
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
            _autoMapper = autoMapper ?? throw new ArgumentNullException(nameof(autoMapper));
        }

        public ValidationResult Validate(PersonRequest request)
        {
            return _validator.Validate(request);
        }

        public async Task<Person> Save(PersonRequest request)
        {
            Person person = _autoMapper.Map<PersonRequest, Person>(request);

            // push to EventGrid
            var eventGridEvent = new EventGridEvent
            {
                Id = Guid.NewGuid().ToString(),
                EventType = "Create Person",
                EventTime = DateTime.Now,
                Subject = "/personService/personCreateRequest",
                Data = person,
                DataVersion = "1.0"
            };

            var topicHost = new Uri("https://localhost:60101/api/events").Authority;
            using var eventGridClient = _eventGridClientFactory.Create("whatever");
            await eventGridClient.PublishEventsAsync(topicHost, new[] { eventGridEvent });

            return person;
        }

        public async Task Process(Person person)
        {
            // save to Cosmos
            await _personRepository.Upsert(person);
        }
    }
}