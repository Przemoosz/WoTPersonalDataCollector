namespace WotPersonalDataCollector.WebApp.Utilities;

internal interface IAspConfiguration
{
    string DatabaseName { get; }
    string WotDtoContainerName { get; }
    string VersionModelContainerName { get; }

	string CosmosConnectionString { get; }
    string WotDtoVersion { get; }
}