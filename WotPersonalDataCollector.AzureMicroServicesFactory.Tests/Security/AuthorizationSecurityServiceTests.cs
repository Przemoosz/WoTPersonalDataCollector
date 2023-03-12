namespace WotPersonalDataCollector.AzureMicroServicesFactory.Tests.Security
{
	using System;
	using Microsoft.Extensions.Logging;
	using WotPersonalDataCollector.AzureMicroServicesFactory.Security;
	using TestHelpers.Categories;
	using System.Reflection;
	using System.Threading;
	using System.Threading.Tasks;

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

		[TestCase(25)]
		[TestCase(12)]
		public void ShouldCountTotalAttempts(int totalAttempts)
		{
			// Act
			for (int i = 0; i < totalAttempts; i++)
			{
				_uut.SaveSecurityCheck(true);
			}
			
			// Assert
			_uut.TotalAttempts.Should().Be(totalAttempts);
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
			EnsureThatAuthorizationIsBlocked();
			typeof(AuthorizationSecurityService).GetProperty("BlockExpireDateTime", BindingFlags.Public | BindingFlags.Instance)!.SetValue(_uut,DateTime.MinValue);

			// Act
			var result = _uut.IsAuthorizationAvailable();

			// Assert
			result.Should().BeTrue();
		}

		[Test]
		public void ShouldAvoidRaceConditionWhenCheckingIfAuthorizationIsAvailableAndOtherThreadBlockingAuthorization()
		{
			// Arrange
			for (int i = 0; i < 4; i++)
			{
				_uut.SaveSecurityCheck(false);
			}

			Task savingTask = new Task(() => _uut.SaveSecurityCheck(false));
			Task<bool> checkingTask = new Task<bool>(() => _uut.IsAuthorizationAvailable());

			// Act
			checkingTask.Start();
			Thread.Sleep(2); // slowing down second task
			savingTask.Start();
			Task.WaitAll(checkingTask, savingTask);

			// Assert
			checkingTask.Result.Should().BeTrue();

		}

		[Test]
		public void ShouldAvoidRaceConditionWhenBlockingAuthorizationAndOtherThreadIsCheckingIfAuthorizationIsAvailable()
		{
			// Arrange
			for (int i = 0; i < 4; i++)
			{
				_uut.SaveSecurityCheck(false);
			}

			Task savingTask = new Task(() => _uut.SaveSecurityCheck(false));
			Task<bool> checkingTask = new Task<bool>(() => _uut.IsAuthorizationAvailable());

			// Act
			savingTask.Start();
			Thread.Sleep(2); // slowing down second task
			checkingTask.Start();
			Task.WaitAll(checkingTask, savingTask);

			// Assert
			checkingTask.Result.Should().BeFalse();
		}
		private void EnsureThatAuthorizationIsBlocked()
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
