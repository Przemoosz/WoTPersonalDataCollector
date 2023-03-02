using System.Reflection;

namespace WotPersonalDataCollector.AzureMicroServicesFactory.Tests.Security
{
	using System;
	using Microsoft.Extensions.Logging;
	using WotPersonalDataCollector.AzureMicroServicesFactory.Security;
	using TestHelpers.Categories;

	[TestFixture, ServiceTest, Parallelizable]
	public class AuthorizationSecurityServiceTests
	{
		private ILogger<AuthorizationSecurityService> _logger;
		private IAuthorizationSecurityService _uut;

		[SetUp]
		public void SetUp()
		{
			_logger = Substitute.For<ILogger<AuthorizationSecurityService>>();
			_uut = new AuthorizationSecurityService(_logger);
		}

		[Test]
		public void AttemptsShouldHaveDefaultValueAfterInitialization()
		{
			// Assert
			_uut.TotalWrongAttempts.Should().Be(0);
			_uut.TotalAttempts.Should().Be(0);
			_uut.BlockExpireDateTime.Should().Be(DateTime.MinValue);
			_uut.MaxWrongAttempts.Should().Be(5);
		}

		[Test]
		public void ShouldIncreaseNumberOfAttemptsForCorrectAuthorization()
		{
			// Act
			_uut.SaveSecurityCheck(true);

			// Assert
			_uut.TotalAttempts.Should().Be(1);
			_uut.TotalWrongAttempts.Should().Be(0);
		}

		[Test]
		public void ShouldIncreaseNumberOfAttemptsForInvalidAuthorization()
		{
			// Act
			_uut.SaveSecurityCheck(false);

			// Assert
			_uut.TotalAttempts.Should().Be(1);
			_uut.TotalWrongAttempts.Should().Be(1);
		}

		[Test]
		public void ShouldBlockAuthorizationAfterFiveWrongAttempts()
		{
			// Arrange
			ForceAuthorizationBlock();
			// Act 
			var result = _uut.IsAuthorizationAvailable();

			// Assert
			result.Should().BeFalse();
		}

		[Test]
		public void AuthorizationShouldBeUnlockedByDefault()
		{
			// Act
			var result = _uut.IsAuthorizationAvailable();

			// Assert
			result.Should().BeTrue();
		}

		[Test]
		public void ShouldUnlockAuthorizationWhenBlockTimeExpires()
		{
			// Arrange
			ForceAuthorizationBlock();
			EnsureBlockedAuthorization();
			typeof(AuthorizationSecurityService).GetProperty("BlockExpireDateTime", BindingFlags.Public | BindingFlags.Instance).SetValue(_uut,DateTime.MinValue);

			// Act
			var result = _uut.IsAuthorizationAvailable();

			// Assert
			result.Should().BeTrue();

		}

		private void EnsureBlockedAuthorization()
		{
			var result = _uut.IsAuthorizationAvailable();
			result.Should().BeFalse();
		}

		private void ForceAuthorizationBlock()
		{
			for (int i = 0; i < _uut.MaxWrongAttempts; i++)
			{
				_uut.SaveSecurityCheck(false);
			}
		}
	}
}
