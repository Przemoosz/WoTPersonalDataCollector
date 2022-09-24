using WotPersonalDataCollector.Api.PersonalData.Dto;

namespace WotPersonalDataCollector.CosmosDb.DTO
{
    internal sealed class WotDataCosmosDbDto
    {
        private const string DtoType = "WotAccount";
        public WotDataCosmosDbDto(WotAccountDto wotAccountDto, string accountId, string dtoVersion)
        {
            ClassProperties = new ClassProperties()
            {
                AccountId = accountId,
                DtoVersion = dtoVersion,
                Type = DtoType
            };
            AccountData = wotAccountDto;
        }
        public WotAccountDto AccountData { get; set; }
        public ClassProperties ClassProperties { get; set; }
    }
}
