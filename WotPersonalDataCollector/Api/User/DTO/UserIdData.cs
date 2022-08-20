using Newtonsoft.Json;

namespace WotPersonalDataCollector.Api.User.DTO;

public class UserIdData
{
    [JsonProperty("Nickname")]
    public string Nickname { get; set; }
    [JsonProperty("account_id")]
    public int AccountId { get; set; }
}