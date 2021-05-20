using System.IO;
using AutoMapper;
using FluentValidation;
using FunctionsPlayground.Models;
using FunctionsPlayground.Services;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(FunctionsPlayground.Functions.Startup))]

namespace FunctionsPlayground.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // NOTE: not doing assembly scanning as don't know the impact on startup time in a functions app

            builder.Services.AddSingleton<IEventGridClientFactory, EventGridClientFactory>();

            // PersonRepository
            builder.AddCustomConfiguration<RepositorySettings>("PersonRepository");
            builder.Services.AddSingleton(serviceProvider => {

                var clientOptions = new CosmosClientOptions
                {
                    SerializerOptions = new CosmosSerializationOptions { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase },
                    AllowBulkExecution = false
                };

                var repositorySettings = serviceProvider.GetRequiredService<RepositorySettings>();
                var cosmosClient = new CosmosClient(repositorySettings.ConnectionString, clientOptions);

                return cosmosClient;
            });
            builder.Services.AddSingleton<IPersonRepository, PersonRepository>();

            // AutoMapper
            builder.Services.AddAutoMapper(expression => 
                expression.AddProfile<PersonProfile>()
            );

            // PersonService
            builder.Services.AddTransient<IValidator<PersonRequest>, PersonRequestValidator>();
            builder.Services.AddTransient<IPersonService, PersonService>();
        }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            var context = builder.GetContext();

            builder.ConfigurationBuilder
                .AddJsonFile(Path.Combine(context.ApplicationRootPath, "local.settings.json"), true, true);
        }
    }
}
