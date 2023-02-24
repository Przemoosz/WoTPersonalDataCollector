namespace WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version;

internal interface ISemanticVersionModelFactory
{
	SemanticVersionModel Create(string version);
}