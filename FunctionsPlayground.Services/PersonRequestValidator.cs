using FluentValidation;
using FunctionsPlayground.Models;

namespace FunctionsPlayground.Services
{
    public class PersonRequestValidator : AbstractValidator<PersonRequest>
    {
        public PersonRequestValidator()
        {
            RuleFor(x => x.Forename).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
        }
    }
}