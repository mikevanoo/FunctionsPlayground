using FluentValidation.Results;
using FunctionsPlayground.Models;

namespace FunctionsPlayground.Services
{
    public interface IPersonService
    {
        ValidationResult Validate(Person person);
    }
}