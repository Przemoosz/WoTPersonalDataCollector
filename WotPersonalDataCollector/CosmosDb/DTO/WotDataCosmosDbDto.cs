using System;
using Newtonsoft.Json;
using WotPersonalDataCollector.Api.PersonalData.Dto;

namespace WotPersonalDataCollector.CosmosDb.DTO
{
    internal sealed class WotDataCosmosDbDto
    {
        private const string DtoType = "WotAccount";

        [JsonProperty("id")]
        public string Id { get; set; }
        public string CreationDate { get; set; }
        public WotAccountDto AccountData { get; set; }

        public ClassProperties ClassProperties { get; set; }
        public string AccountId { get; set; }

        public WotDataCosmosDbDto()
        {
            
        }
        public WotDataCosmosDbDto(WotAccountDto wotAccountDto, string accountId, string dtoVersion)
        {
            AccountId = accountId;
            ClassProperties = new ClassProperties()
            {
                DtoVersion = dtoVersion,
                Type = DtoType
            };
            AccountData = wotAccountDto;
            Id = Guid.NewGuid().ToString("D");
            CreationDate = DateTime.UtcNow.ToString("G");
        }
    }
}
