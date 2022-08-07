using Newtonsoft.Json;

namespace WotPersonalDataCollector.Api.User.DTO;

public class UserData
{
    [JsonProperty("Nickname")]
    public string Nickname { get; set; }
    [JsonProperty("accoutn_id")]
    public int AccountId { get; set; }
}