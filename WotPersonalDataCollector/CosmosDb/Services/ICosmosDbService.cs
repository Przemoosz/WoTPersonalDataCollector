using System.Threading.Tasks;
using WotPersonalDataCollector.CosmosDb.DTO;

namespace WotPersonalDataCollector.CosmosDb.Services;

internal interface ICosmosDbService
{
    Task SaveAsync(WotDataCosmosDbDto wotDataCosmosDbDto);
}