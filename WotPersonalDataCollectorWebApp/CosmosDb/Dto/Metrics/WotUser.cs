using Newtonsoft.Json;

namespace WotPersonalDataCollectorWebApp.CosmosDb.Dto.Metrics
{
    public class WotUser
    {
        [JsonProperty("last_battle_time")]
        public int LastBattleTime { get; set; }
        [JsonProperty("created_at")]
        public int CreatedTime { get; set; }
        [JsonProperty("updated_at")]
        public int UpdatedTime { get; set; }
        [JsonProperty("global_rating")]
        public int GlobalRating { get; set; }
        [JsonProperty("statistics")]
        public Statistics Statistics { get; set; }
        [JsonProperty("logout_at")]
        public int LogoutTime { get; set; }
    }
}
