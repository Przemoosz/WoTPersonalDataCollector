namespace WotPersonalDataCollector.AzureMicroServicesFactory.Authentication.Token
{
	/// <summary>
	/// Defines methods for authentication token factory.
	/// </summary>
	internal interface ITokenFactory
	{
		/// <summary>
		/// Creates basic authentication token based on provided in settings username and password.
		/// </summary>
		/// <returns><see cref="string"/> value of basic authentication token.</returns>
		string CreateBasicToken();
	}
}