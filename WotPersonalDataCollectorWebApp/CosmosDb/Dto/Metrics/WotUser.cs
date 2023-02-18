namespace WotPersonalDataCollectorWebApp.CosmosDb.Dto.Metrics
{
    public class WotUser
    {
        public int LastBattleTime { get; set; }
        public int CreatedTime { get; set; }
        public int UpdatedTime { get; set; }
        public int GlobalRating { get; set; }
        public Statistics Statistics { get; set; } 
        public int LogoutTime { get; set; }
    }
}
