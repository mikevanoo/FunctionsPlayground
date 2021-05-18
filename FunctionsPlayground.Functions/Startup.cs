using FunctionsPlayground.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(FunctionsPlayground.Functions.Startup))]

namespace FunctionsPlayground.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<IPersonService>(sp => new PersonService(new PersonValidator()));
        }
    }
}
