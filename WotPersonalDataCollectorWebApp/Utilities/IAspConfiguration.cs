namespace WotPersonalDataCollectorWebApp.Utilities;

internal interface IAspConfiguration
{
    string DatabaseName { get; }
    string ContainerName { get; }
    string CosmosConnectionString { get; }
    string WotDtoVersion { get; }
}