namespace WotPersonalDataCollector.AzureMicroServicesFactory.Security
{
	using System;
	using Microsoft.Extensions.Logging;

	/// <inheritdoc/>
	internal sealed class AuthorizationSecurityService: IAuthorizationSecurityService
	{
		private readonly ILogger<AuthorizationSecurityService> _logger;
		private readonly object _securityLock = new();
		private bool IsAuthorizationBlocked { get; set; }
		public int TotalAttempts { get; private set; }
		public int TotalWrongAttempts { get; private set; }
		public int MaxWrongAttempts => 5;
		public DateTime BlockExpireDateTime { get; private set; } = DateTime.MinValue;

		public AuthorizationSecurityService(ILogger<AuthorizationSecurityService> logger)
		{
			_logger = logger;
		}

		public void SaveSecurityCheck(bool isAuthorizedCorrectly)
		{
			lock (_securityLock)
			{
				TotalAttempts++;
				if (isAuthorizedCorrectly)
				{
					return;
				}

				TotalWrongAttempts++;
				if (TotalWrongAttempts == 5)
				{
					IsAuthorizationBlocked = true;
					BlockExpireDateTime = DateTime.UtcNow + TimeSpan.FromMinutes(5);
				}
			}
		}

		public bool IsAuthorizationAvailable()
		{
			lock (_securityLock)
			{
				if (IsAuthorizationBlocked)
				{
					return TryReleaseAuthorizationBlock();
				}
				return true;
			}
		}

		private bool TryReleaseAuthorizationBlock()
		{
			if (DateTime.UtcNow > BlockExpireDateTime)
			{
				_logger.LogInformation("Authorization via Basic token is now available. Block released.");
				IsAuthorizationBlocked = false;
				TotalWrongAttempts = 0;
				return true;
			}
			_logger.LogWarning("Authorization blockade can not be released!");
			return false;
		}
	}
}