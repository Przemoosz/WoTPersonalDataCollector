namespace WotPersonalDataCollector.Api;

internal interface IApiUrlFactory
{
    string Create(string baseUrl);
}