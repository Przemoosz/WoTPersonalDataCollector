using WotPersonalDataCollector.Api.Http.RequestObjects;

namespace WotPersonalDataCollector.Api;

internal interface IApiUriFactory
{
    string Create(string baseUrl, IRequestObject requestObject);
}