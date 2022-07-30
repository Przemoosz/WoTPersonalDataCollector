namespace WotPersonalDataCollector.Utilities;

internal interface IConfiguration
{
    public string ApplicationId { get; }
    public string UserName { get; }
    bool TryGetUserName(out string userName);
}