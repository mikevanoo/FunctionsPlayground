using System.Threading.Tasks;
using FluentValidation.Results;
using FunctionsPlayground.Models;

namespace FunctionsPlayground.Services
{
    public interface IPersonService
    {
        ValidationResult Validate(PersonRequest request);

        Task<Person> Save(PersonRequest request);
    }
}