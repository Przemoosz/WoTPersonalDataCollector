namespace WotPersonalDataCollector.AzureMicroServicesFactory.Security.Validation
{
	/// <summary>
	/// Defines method for validating if provided authorization token have correct form and if so then checks if token
	/// matches token provided by application.
	/// </summary>
	internal interface IAuthorizationTokenValidationService
	{
		/// <summary>
		/// Validates if token have correct form and validates if token matches internal generated token.
		/// </summary>
		/// <param name="providedToken">Token from HTTP request header.</param>
		/// <returns><see cref="bool"/> value that describes if token is correct.</returns>
		bool Validate(string providedToken);
	}
}