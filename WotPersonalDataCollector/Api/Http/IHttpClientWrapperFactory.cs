namespace WotPersonalDataCollector.Api.Http;

internal interface IHttpClientWrapperFactory
{
    IHttpClientWrapper Create();
}