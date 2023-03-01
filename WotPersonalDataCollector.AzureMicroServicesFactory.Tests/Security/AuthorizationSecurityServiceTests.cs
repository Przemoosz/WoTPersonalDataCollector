namespace WotPersonalDataCollector.AzureMicroServicesFactory.Tests.Security
{
	using System;
	using Microsoft.Extensions.Logging;
	using WotPersonalDataCollector.AzureMicroServicesFactory.Security;
	using Categories;

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
			_uut.IsAuthorizationBlocked.Should().BeFalse();
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
			// Act 
			ForceAuthorizationBlock();

			// Assert
			_uut.IsAuthorizationBlocked.Should().BeTrue();
		}

		[Test]
		public void 

		private void ForceAuthorizationBlock()
		{
			for (int i = 0; i < _uut.MaxWrongAttempts; i++)
			{
				_uut.SaveSecurityCheck(false);
			}
		}
	}
}
