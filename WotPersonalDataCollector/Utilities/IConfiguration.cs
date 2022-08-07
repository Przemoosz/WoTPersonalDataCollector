namespace WotPersonalDataCollector.Utilities;

internal interface IConfiguration
{
    public string ApplicationId { get; }
    public string UserId { get; set; }
    public string UserName { get; }
    bool TryGetUserName(out string userName);
}