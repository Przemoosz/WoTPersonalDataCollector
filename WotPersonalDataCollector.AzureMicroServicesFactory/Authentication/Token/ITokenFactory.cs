namespace WotPersonalDataCollector.AzureMicroServicesFactory.Authentication.Token;

internal interface ITokenFactory
{
	string CreateBasicToken();
}