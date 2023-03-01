using Microsoft.AspNetCore.Http;

namespace WotPersonalDataCollector.AzureMicroServicesFactory.Authorization;

internal interface IAuthorizationService
{ 
	void Authorize(HttpRequest httpRequest);
}