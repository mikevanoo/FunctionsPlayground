using System;
using FluentValidation;
using FluentValidation.Results;
using FunctionsPlayground.Models;

namespace FunctionsPlayground.Services
{
    public class PersonService : IPersonService
    {
        private readonly AbstractValidator<Person> _validator;

        public PersonService(AbstractValidator<Person> validator)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public ValidationResult Validate(Person person)
        {
            return _validator.Validate(person);
        }
    }
}