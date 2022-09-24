using WotPersonalDataCollector.Api.PersonalData.Dto;

namespace WotPersonalDataCollector.CosmosDb.DTO
{
    internal sealed class WotDataCosmosDbDto
    {
        public WotAccountDto AccountData { get; set; }
        public ClassProperties ClassProperties { get; set; }
    }

    internal sealed class ClassProperties
    {
        public string Type { get; set; }
        public string TypeVersion { get; set; }
        public string AccountId { get; set; }
    }
}
