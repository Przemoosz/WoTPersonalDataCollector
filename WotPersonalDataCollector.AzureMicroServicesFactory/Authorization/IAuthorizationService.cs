using Microsoft.AspNetCore.Http;

namespace WotPersonalDataCollector.AzureMicroServicesFactory.Authorization;

internal interface IAuthorizationService
{ 
	bool IsAuthorized(HttpRequest httpRequest);
}