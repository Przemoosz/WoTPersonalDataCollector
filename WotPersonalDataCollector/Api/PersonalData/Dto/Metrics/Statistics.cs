using Newtonsoft.Json;

namespace WotPersonalDataCollector.Api.PersonalData.Dto.Metrics;

public class Statistics
{
    [JsonProperty("all")]
    public AccountStatistics AccountStatistics { get; set; }
    [JsonProperty("trees_cut")]
    public int TreesCut { get; set; }
}