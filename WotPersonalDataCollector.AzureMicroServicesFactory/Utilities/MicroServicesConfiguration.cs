using Microsoft.Extensions.Logging;

namespace WotPersonalDataCollector.AzureMicroServicesFactory.Utilities
{
	using GuardNet;
	using System;
	using SharedKernel.Exceptions;
	using WotPersonalDataCollector.SharedKernel.Utilities;

	/// <summary>
	/// Implementation of <see cref="IMicroServicesConfiguration"/> interface. Provides values of local variables.
	/// </summary>
	internal class MicroServicesConfiguration : ConfigurationBase, IMicroServicesConfiguration
	{
		private readonly ILogger<MicroServicesConfiguration> _logger;

		public MicroServicesConfiguration(ILogger<MicroServicesConfiguration> logger)
		{
			_logger = logger;
		}
		/// <inheritdoc/>
		/// <exception cref="LocalVariableException"/>
		public string AdminUsername
		{
			get
			{
				_logger.LogError("dsdsds");
				_logger.LogDebug("chuj kurwa i chuj");
				Guard.NotNullOrEmpty<LocalVariableException>(Environment.GetEnvironmentVariable("AdminUsername"), "AdminUsername local variable is not set!");
				return Environment.GetEnvironmentVariable("AdminUsername");
			}
		}

		/// <inheritdoc/>
		/// <exception cref="LocalVariableException"/>
		public string AdminPassword
		{
			get
			{
				Guard.NotNullOrEmpty<LocalVariableException>(Environment.GetEnvironmentVariable("AdminPassword"), "AdminPassword local variable is not set!");
				return Environment.GetEnvironmentVariable("AdminPassword");
			}
		}
	}
}

