namespace WotPersonalDataCollectorWebApp.CosmosDb.Dto.Version;

internal interface IDtoVersionFactory
{
	DtoVersion Create(string version);
}