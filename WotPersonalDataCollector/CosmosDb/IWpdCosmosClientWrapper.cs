using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace WotPersonalDataCollector.CosmosDb;

internal interface IWpdCosmosClientWrapper: IDisposable
{
    Task<Database> CreateDatabaseIfNotExistsAsync();
}