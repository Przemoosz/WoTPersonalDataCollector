namespace WotPersonalDataCollector.Api.Http.RequestObjects
{
    internal class UserPersonalDataRequestObject: IRequestObject
    {
        public string application_id { get; init; }
        public string account_id { get; set; }
    }
}
