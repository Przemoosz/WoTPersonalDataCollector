using Newtonsoft.Json;

namespace WotPersonalDataCollectorWebApp.CosmosDb.Dto
{
    public class Statistics
    {
        [JsonProperty("all")]
        public AccountStatistics AccountStatistics { get; set; }
        [JsonProperty("trees_cut")]
        public int TreesCut { get; set; }
    }
}
