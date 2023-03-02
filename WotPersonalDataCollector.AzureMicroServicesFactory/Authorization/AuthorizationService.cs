namespace WotPersonalDataCollector.AzureMicroServicesFactory.Authorization
{
	using System;
	using System.Linq;
	using Microsoft.AspNetCore.Http;
	using Authentication.Token;
	using Extensions;

	internal class AuthorizationService : IAuthorizationService
    {
	    private readonly ITokenFactory _tokenFactory;

	    public AuthorizationService(ITokenFactory tokenFactory)
	    {
		    _tokenFactory = tokenFactory;
	    }
        public void Authorize(HttpRequest httpRequest)
        {
	        var authorizationToken = httpRequest.GetAuthorizationToken();
	        Console.WriteLine(authorizationToken.First());

        }

        private void ValidateTokenFormat(string token)
        {
			token.spli
        }
    }
}
