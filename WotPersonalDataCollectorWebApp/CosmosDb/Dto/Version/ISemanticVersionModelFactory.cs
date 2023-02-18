namespace WotPersonalDataCollectorWebApp.CosmosDb.Dto.Version;

internal interface ISemanticVersionModelFactory
{
	SemanticVersionModel Create(string version);
}