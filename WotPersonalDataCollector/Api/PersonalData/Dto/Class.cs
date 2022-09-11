using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WotPersonalDataCollector.Api.Dto
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

    public class WotUser
    {
        public int last_battle_time { get; set; }
        public int created_at { get; set; }
        public int updated_at { get; set; }
        public int global_rating { get; set; }
        public int clan_id { get; set; }
        public Statistics statistics { get; set; }
        public string nickname { get; set; }
        public int logout_at { get; set; }
    }

    public class AccountStatistics
    {
        public int spotted { get; set; }
        public int battles_on_stunning_vehicles { get; set; }
        public int max_xp { get; set; }
        public double avg_damage_blocked { get; set; }
        public int direct_hits_received { get; set; }
        public int explosion_hits { get; set; }
        public int piercings_received { get; set; }
        public int piercings { get; set; }
        public int max_damage_tank_id { get; set; }
        public int xp { get; set; }
        public int survived_battles { get; set; }
        public int dropped_capture_points { get; set; }
        public int hits_percents { get; set; }
        public int draws { get; set; }
        public int max_xp_tank_id { get; set; }
        public int battles { get; set; }
        public int damage_received { get; set; }
        public double avg_damage_assisted { get; set; }
        public int max_frags_tank_id { get; set; }
        public double avg_damage_assisted_track { get; set; }
        public int frags { get; set; }
        public int stun_number { get; set; }
        public double avg_damage_assisted_radio { get; set; }
        public int capture_points { get; set; }
        public int stun_assisted_damage { get; set; }
        public int max_damage { get; set; }
        public int hits { get; set; }
        public int battle_avg_xp { get; set; }
        public int wins { get; set; }
        public int losses { get; set; }
        public int damage_dealt { get; set; }
        public int no_damage_direct_hits_received { get; set; }
        public int max_frags { get; set; }
        public int shots { get; set; }
        public int explosion_hits_received { get; set; }
        public double tanking_factor { get; set; }
    }
    
    public class Data
    {
        public WotUser WotUser { get; set; }
    }

    
    public class WotAccountDto
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public class Statistics
    {
        [JsonProperty("all")]
        public AccountStatistics AccountStatistics { get; set; }
        [JsonProperty("trees_cut")]
        public int TreesCut { get; set; }
    }
}
