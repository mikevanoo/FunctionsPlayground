using FluentValidation;
using FunctionsPlayground.Models;

namespace FunctionsPlayground.Services
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Forename).NotEmpty();
            RuleFor(x => x.Forename).Must(x => x.Contains("xxx")).WithMessage("'{PropertyName}' must contain 'xxx'.");
            RuleFor(x => x.Surname).NotEmpty();
        }
    }
}