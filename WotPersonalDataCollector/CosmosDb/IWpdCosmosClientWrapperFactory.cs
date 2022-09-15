namespace WotPersonalDataCollector.CosmosDb;

internal interface IWpdCosmosClientWrapperFactory
{
    IWpdCosmosClientWrapper Create();
}