using WotPersonalDataCollector.Api.User.DTO;

namespace WotPersonalDataCollector.Api.Http.RequestObjects;

internal interface IUserPersonalDataRequestObjectFactory
{
    IRequestObject Create(UserIdData userIdData);
}