using WotPersonalDataCollector.AzureMicroServicesFactory.Authentication.Token;

namespace WotPersonalDataCollector.AzureMicroServicesFactory.Security.Validation
{
    internal sealed class AuthorizationTokenValidationService: IAuthorizationTokenValidationService
	{
	    private readonly ITokenFactory _tokenFactory;

	    public AuthorizationTokenValidationService(ITokenFactory tokenFactory)
	    {
		    _tokenFactory = tokenFactory;
	    }

	    public bool Validate(string providedToken)
	    {

		    return true;
	    }
    }

    internal interface IAuthorizationTokenValidationService
    {
    }
}
