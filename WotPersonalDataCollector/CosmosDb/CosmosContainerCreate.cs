using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace WotPersonalDataCollector.CosmosDb
{
    internal sealed class CosmosContainerCreate: ICosmosContainerCreate
    {
        private const string PartitionKey = "/";
        public async Task<Container> Create(Database database)
        {
            ContainerProperties properties = new ContainerProperties();
            // database.CreateContainerIfNotExistsAsync()
            return null;
        }
    }

    internal interface ICosmosContainerCreate
    {
    }
}
