namespace WotPersonalDataCollector.Utilities;

internal interface IConfiguration
{
    public string ApplicationId { get; }
    public string UserId { get; set; }
    public string UserName { get; }
    public string PersonalDataUri { get; }
    public string PlayersUri { get; }
    public string CosmosConnectionString { get; }
    public string CosmosDbName { get; }
    public int DatabaseThroughput { get; }
    public string ContainerName { get; }
    public string DtoVersion { get; }
    bool TryGetUserName(out string userName);
}