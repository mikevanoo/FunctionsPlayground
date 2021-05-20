using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FunctionsPlayground.Functions
{
    public static class HostBuilderExtensions
    {
        public static IFunctionsHostBuilder AddCustomConfiguration<TConfig>(this IFunctionsHostBuilder builder, string sectionName)
            where TConfig : class
        {
            builder.Services.AddSingleton(svc =>
            {
                var section = builder.GetContext().Configuration.GetSection(sectionName);

                return section.Get<TConfig>();
            });

            return builder;
        }
    }
}