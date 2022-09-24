using WotPersonalDataCollector.Api.PersonalData.Dto;
using WotPersonalDataCollector.Api.User.DTO;

namespace WotPersonalDataCollector.CosmosDb.DTO;

internal interface IWotDataCosmosDbDtoFactory
{
    WotDataCosmosDbDto Create(WotAccountDto accountDto, UserIdData userIdData);
}