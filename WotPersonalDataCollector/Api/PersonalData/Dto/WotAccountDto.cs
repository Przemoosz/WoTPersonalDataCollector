using Newtonsoft.Json;
using WotPersonalDataCollector.Api.PersonalData.Dto.Metrics;

namespace WotPersonalDataCollector.Api.PersonalData.Dto
{
    public class WotAccountDto
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("data")]
        public Data Data { get; set; }
    }
}
