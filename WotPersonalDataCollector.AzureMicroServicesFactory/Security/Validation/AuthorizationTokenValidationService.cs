namespace WotPersonalDataCollector.AzureMicroServicesFactory.Security.Validation
{
	using Authentication.Token;
	using Microsoft.Extensions.Logging;

	/// <summary>
	/// Implementation of <see cref="IAuthorizationTokenValidationService"/> interface. Checks if provided token is in correct form and valid.
	/// </summary>
	internal sealed class AuthorizationTokenValidationService: IAuthorizationTokenValidationService
	{
		private const string TokenType = "Basic";
	    private readonly ITokenFactory _tokenFactory;
	    private readonly ILogger<AuthorizationTokenValidationService> _logger;

	    public AuthorizationTokenValidationService(ITokenFactory tokenFactory, ILogger<AuthorizationTokenValidationService> logger)
	    {
		    _tokenFactory = tokenFactory;
		    _logger = logger;
	    }

		public bool Validate(string providedToken)
	    {
			var tokenParts = providedToken.Split(' ');
			if (tokenParts.Length != 2)
			{
				_logger.LogError("Provided token that contains less or more than two pieces. Token should have exactly two parts!");
				return false;
			}
			if (!tokenParts[0].Equals(TokenType))
			{
				_logger.LogError("Provided token is not type of basic token.");
				return false;
			}
			return providedToken.Equals(_tokenFactory.CreateBasicToken());
	    }
    }
}
