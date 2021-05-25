using System;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Messaging.EventGrid;
using FluentValidation;
using FluentValidation.Results;
using FunctionsPlayground.Models;

namespace FunctionsPlayground.Services
{
    public class PersonService : IPersonService
    {
        private readonly IValidator<PersonRequest> _validator;
        private readonly IEventGridPublisherClientFactory _eventGridClientFactory;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _autoMapper;

        public PersonService(
            IValidator<PersonRequest> validator,
            IEventGridPublisherClientFactory eventGridClientFactory,
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
            var eventGridEvent = new EventGridEvent(
                subject: "/personService/personCreateRequest",
                eventType: "Create Person", 
                dataVersion: "1.0", 
                data: new BinaryData(person))
            {
                Id = Guid.NewGuid().ToString(), 
                EventTime = DateTime.UtcNow
            };
            
            var eventGridClient = _eventGridClientFactory.Create("https://localhost:60101/api/events", "whatever");
            await eventGridClient.SendEventAsync(eventGridEvent);

            return person;
        }

        public async Task Process(Person person)
        {
            // save to Cosmos
            await _personRepository.Upsert(person);
        }
    }
}