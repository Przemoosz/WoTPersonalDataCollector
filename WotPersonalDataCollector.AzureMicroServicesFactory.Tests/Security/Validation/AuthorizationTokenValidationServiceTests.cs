namespace WotPersonalDataCollector.AzureMicroServicesFactory.Tests.Security.Validation
{
	using System;
	using WotPersonalDataCollector.AzureMicroServicesFactory.Authentication.Token;
	using Exceptions;
	using WotPersonalDataCollector.AzureMicroServicesFactory.Security.Validation;
	using TestHelpers.Categories;

	[TestFixture, ServiceTest, Parallelizable]
	public class AuthorizationTokenValidationServiceTests
	{
		private ITokenFactory _tokenFactory;
		private AuthorizationTokenValidationService _uut;

		[SetUp]
		public void SetUp()
		{
			_tokenFactory = Substitute.For<ITokenFactory>();
			_uut = new AuthorizationTokenValidationService(_tokenFactory);
		}

		[Test]
		public void ShouldReturnTrueIfTokenIsValid()
		{
			// Arrange
			const string token = "Basic xxx";
			_tokenFactory.CreateBasicToken().Returns(token);
			
			// Act
			var actual = _uut.Validate(token);

			// Assert
			actual.Should().BeTrue();
		}

		[Test]
		public void ShouldReturnFalseIfTokenIsNotValid()
		{
			// Arrange
			const string token = "Basic xxx";
			const string providedToken = "Basic yyy";
			_tokenFactory.CreateBasicToken().Returns(token);

			// Act
			var actual = _uut.Validate(providedToken);

			// Assert
			actual.Should().BeFalse();
		}

		[Test]
		public void ShouldThrowTokenValidationExceptionIfTokenIsNotInTwoPiece()
		{
			// Arrange
			const string providedToken = "InValidToken";

			// Act
			Func<bool> actual = () => _uut.Validate(providedToken);

			// Assert
			actual.Should().Throw<TokenValidationException>().WithMessage("Expected two-piece token.");
		}

		[Test]
		public void ShouldThrowTokenValidationExceptionIfTokenIsNotBasicType()
		{
			// Arrange
			const string providedToken = "Bearer Token";

			// Act
			Func<bool> actual = () => _uut.Validate(providedToken);

			// Assert
			actual.Should().Throw<TokenValidationException>().WithMessage("Expected basic type token.");
		}
	}
}
