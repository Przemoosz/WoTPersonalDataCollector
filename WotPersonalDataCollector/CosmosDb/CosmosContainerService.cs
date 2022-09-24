using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using WotPersonalDataCollector.Utilities;

namespace WotPersonalDataCollector.CosmosDb
{
    internal sealed class CosmosContainerService: ICosmosContainerService
    {
        private const string PartitionKey = @"/ClassProperties/AccountId";
        private readonly IConfiguration _configuration;

        public CosmosContainerService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public async Task Create(Database database)
        {
            ContainerProperties properties = new ContainerProperties(_configuration.ContainerName, PartitionKey);
            await database.CreateContainerIfNotExistsAsync(properties);
        }
    }
}
