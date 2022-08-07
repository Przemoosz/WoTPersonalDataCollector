namespace WotPersonalDataCollector.Api.Http.RequestObjects
{
    internal class UserInfoRequestObject: IRequestObject
    {
        public string application_id { get; init; }
        public string search { get; set; }
    }
}
