namespace WotPersonalDataCollector.Api.Http;

internal interface IHttpClientWrapperFactory
{
    HttpClientWrapper Create();
}