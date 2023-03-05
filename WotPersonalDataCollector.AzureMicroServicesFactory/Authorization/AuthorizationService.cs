namespace WotPersonalDataCollector.AzureMicroServicesFactory.Authorization
{
	using Microsoft.AspNetCore.Http;
	using Extensions;
	using Security;
	using Security.Validation;

	internal class AuthorizationService : IAuthorizationService
    {
	    private readonly IAuthorizationTokenValidationService _authorizationTokenValidationService;
	    private readonly IAuthorizationSecurityService _authorizationSecurityService;

	    public AuthorizationService(IAuthorizationTokenValidationService authorizationTokenValidationService,
		    IAuthorizationSecurityService authorizationSecurityService)
	    {
		    _authorizationTokenValidationService = authorizationTokenValidationService;
		    _authorizationSecurityService = authorizationSecurityService;
	    }
        public bool IsAuthorized(HttpRequest httpRequest)
        {
	        var authorizationToken = httpRequest.GetAuthorizationToken();
	        bool isAuthorized = _authorizationSecurityService.IsAuthorizationAvailable() &&
	                            _authorizationTokenValidationService.Validate(authorizationToken);
			_authorizationSecurityService.SaveSecurityCheck(isAuthorized);
			return isAuthorized;
        }
    }
}
