namespace WotPersonalDataCollector.AzureMicroServicesFactory.Tests.Authentication.Token
{
	using Categories;
	using WotPersonalDataCollector.AzureMicroServicesFactory.Authentication.Token;
	using WotPersonalDataCollector.AzureMicroServicesFactory.Utilities;

	[TestFixture, FactoryTest, Parallelizable]
	public class TokenFactoryTests
	{
		private IMicroServicesConfiguration _configuration;
		private TokenFactory _uut;

		[SetUp]
		public void SetUp()
		{
			_configuration = Substitute.For<IMicroServicesConfiguration>();
			_uut = new TokenFactory(_configuration);
		}

		[TestCase("User", "Password", "Basic VXNlcjpQYXNzd29yZA==")]
		[TestCase("C0mplic4tedUser[name123_", "DidNot_Expect-ThatKind.Of;P44sword", "Basic QzBtcGxpYzR0ZWRVc2VyW25hbWUxMjNfOkRpZE5vdF9FeHBlY3QtVGhhdEtpbmQuT2Y7UDQ0c3dvcmQ=")]
		public void ShouldGenerateBasicToken(string username, string password, string expectedToken)
		{
			// Arrange
			_configuration.AdminUsername.Returns(username);
			_configuration.AdminPassword.Returns(password);

			// Act
			var token = _uut.CreateBasicToken();

			// Assert
			token.Should().Be(expectedToken);
		}
	}
}
