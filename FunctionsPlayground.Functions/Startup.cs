using AutoMapper;
using FluentValidation;
using FunctionsPlayground.Models;
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
            // NOTE: not doing assembly scanning as don't know the impact on startup time in a functions app

            // PersonService
            builder.Services.AddTransient<AbstractValidator<PersonRequest>, PersonRequestValidator>();
            builder.Services.AddTransient<IPersonService, PersonService>();

            // AutoMapper
            builder.Services.AddAutoMapper(expression => 
                expression.AddProfile<PersonProfile>()
            );
        }
    }
}
