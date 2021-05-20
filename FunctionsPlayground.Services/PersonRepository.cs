using System;
using System.Threading.Tasks;
using FunctionsPlayground.Models;
using Microsoft.Azure.Cosmos;

namespace FunctionsPlayground.Services
{
    public class PersonRepository : IPersonRepository
    {
        private readonly RepositorySettings _settings;
        private readonly Database _cosmosDatabase;

        private static readonly ItemRequestOptions SkipEntityResponseRequestOptions = new ItemRequestOptions { EnableContentResponseOnWrite = false };
        
        public PersonRepository(RepositorySettings settings, CosmosClient cosmosClient)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));

            _cosmosDatabase = cosmosClient.GetDatabase(_settings.DatabaseName);
        }

        public async Task Upsert(Person person)
        {
            var container = _cosmosDatabase.GetContainer(_settings.ContainerName);
            await container.UpsertItemAsync(person, null, SkipEntityResponseRequestOptions);
        }
    }
}