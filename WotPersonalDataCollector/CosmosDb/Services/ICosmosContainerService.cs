using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace WotPersonalDataCollector.CosmosDb.Services;

internal interface ICosmosContainerService
{
    Task Create(Database database);
}