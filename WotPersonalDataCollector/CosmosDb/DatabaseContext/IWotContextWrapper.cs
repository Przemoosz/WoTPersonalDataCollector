using System;
using System.Threading.Tasks;
using WotPersonalDataCollector.CosmosDb.DTO;

namespace WotPersonalDataCollector.CosmosDb.DatabaseContext;

internal interface IWotContextWrapper: IAsyncDisposable
{
    Task AddPersonalDataAsync(WotDataCosmosDbDto wotDataCosmosDbDto);
    Task SaveChangesAsync();
}