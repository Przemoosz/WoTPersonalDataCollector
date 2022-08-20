using System.Collections.Generic;

namespace WotPersonalDataCollector.Api.User.DTO
{
    public class ResponseObject
    {
        public string status { get; set; }
        public Meta meta { get; set; }
        public List<UserIdData> data { get; set; }
    }
}
