using System;
using System.Linq;

namespace WotPersonalDataCollector.AzureMicroServicesFactory.Authorization
{
	using Microsoft.AspNetCore.Http;

	internal class AuthorizationService : IAuthorizationService
    {
        public void Authorize(HttpRequest httpRequest)
        {
	        var authorizationToken = httpRequest.Headers["Authorization"];
	        Console.WriteLine(authorizationToken.First());

        }
    }
}
