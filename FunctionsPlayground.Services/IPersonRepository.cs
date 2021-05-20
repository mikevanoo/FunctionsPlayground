using System.Threading.Tasks;
using FunctionsPlayground.Models;

namespace FunctionsPlayground.Services
{
    public interface IPersonRepository
    {
        Task Upsert(Person person);
    }
}