namespace WotPersonalDataCollector.CosmosDb.DatabaseContext;

internal interface IWotContextWrapperFactory
{
    public WotContextWrapper Create();
}