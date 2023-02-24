using WotPersonalDataCollector.Api.PersonalData.Dto;
using WotPersonalDataCollector.Api.User.DTO;
using WotPersonalDataCollector.Utilities;

namespace WotPersonalDataCollector.CosmosDb.DTO
{
    internal sealed class WotDataCosmosDbDtoFactory: IWotDataCosmosDbDtoFactory
    {
        private readonly IConfiguration _configuration;

        public WotDataCosmosDbDtoFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public WotDataCosmosDbDto Create(WotAccountDto accountDto, UserIdData userIdData)
        {
            WotDataCosmosDbDto dto =
                new WotDataCosmosDbDto(accountDto, userIdData.AccountId.ToString(), _configuration.WotDtoVersion);
            return dto;
        }
    }
}
