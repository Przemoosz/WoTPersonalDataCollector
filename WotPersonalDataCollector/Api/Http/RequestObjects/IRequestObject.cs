namespace WotPersonalDataCollector.Api.Http.RequestObjects;

internal interface IRequestObject
{
    public string application_id { get; }
}

public class RequestObject : IRequestObject
{
    public string application_id { get; init; }
}