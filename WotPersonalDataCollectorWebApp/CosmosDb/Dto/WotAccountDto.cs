using Newtonsoft.Json;

namespace WotPersonalDataCollectorWebApp.CosmosDb.Dto
{
    public class WotAccountDto
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("data")]
        public Data Data { get; set; }
    }
}
