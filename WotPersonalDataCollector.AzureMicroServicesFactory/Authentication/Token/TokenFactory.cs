namespace WotPersonalDataCollector.AzureMicroServicesFactory.Authentication.Token
{
	using System.Text;
	using Utilities;

	/// <summary>
	///	Implementation of <see cref="ITokenFactory"/> interface. Provides method that will create basic authentication token.
	/// </summary>
	/// <inheritdoc/>
	internal class TokenFactory: ITokenFactory
	{
		private readonly IMicroServicesConfiguration _configuration;

		public TokenFactory(IMicroServicesConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string CreateBasicToken()
		{
			string encodedCredentials = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
				.GetBytes(_configuration.AdminUsername + ":" + _configuration.AdminPassword));
			return $"Basic {encodedCredentials}";
		}
	}
}
