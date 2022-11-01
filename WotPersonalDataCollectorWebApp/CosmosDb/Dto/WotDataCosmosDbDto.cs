using Newtonsoft.Json;
using WotPersonalDataCollectorWebApp.CosmosDb.Dto.Metrics;

namespace WotPersonalDataCollectorWebApp.CosmosDb.Dto
{
    public sealed class WotDataCosmosDbDto
    {
        private const string DtoType = "WotAccount";

        [JsonProperty("id")] 
        public string Id { get; set; }
        public string CreationDate { get; set; }
        public WotAccountDto AccountData { get; set; }
        public ClassProperties ClassProperties { get; set; }
        public string AccountId { get; set; }
    }
}
