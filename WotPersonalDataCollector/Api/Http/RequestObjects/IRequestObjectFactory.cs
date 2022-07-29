using WotPersonalDataCollector.Api.Http.RequestObjects;

namespace WotPersonalDataCollector.Api.Http.RequestObjects;

internal interface IRequestObjectFactory
{
    IRequestObject Create();
}