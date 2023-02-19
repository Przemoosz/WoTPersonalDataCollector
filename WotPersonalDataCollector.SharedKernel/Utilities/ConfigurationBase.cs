namespace WotPersonalDataCollector.SharedKernel.Utilities
{
	using GuardNet;
	using Exceptions;

	/// <summary>
	/// Implementation of <see cref="IConfigurationBase"/> interface. Provides values of local variables.
	/// </summary>
	public abstract class ConfigurationBase: IConfigurationBase
	{
		/// <inheritdoc />
		/// <exception cref="LocalVariableException"/>
		public string CosmosConnectionString
		{
			get
			{
				Guard.NotNullOrEmpty<LocalVariableException>(Environment.GetEnvironmentVariable("CosmosConnectionString"), "CosmosConnectionString local variable is not set!");
				return Environment.GetEnvironmentVariable("CosmosConnectionString");
			}
		}
		/// <inheritdoc />
		/// <exception cref="LocalVariableException"/>
		public string DatabaseName
		{
			get
			{
				Guard.NotNullOrEmpty<LocalVariableException>(Environment.GetEnvironmentVariable("DatabaseName"), "DatabaseName local variable is not set!");
				return Environment.GetEnvironmentVariable("DatabaseName");
			}
		}

		/// <inheritdoc />
		/// <exception cref="LocalVariableException"/>
		public string WotDtoVersion
		{
			get
			{
				Guard.NotNullOrEmpty<LocalVariableException>(Environment.GetEnvironmentVariable("WotDtoVersion"), "WotDtoVersion local variable is not set!");
				return Environment.GetEnvironmentVariable("WotDtoVersion")!;
			}
		}

		/// <inheritdoc />
		/// <exception cref="LocalVariableException"/>
		public string VersionModelContainerName
		{
			get
			{
				Guard.NotNullOrEmpty<LocalVariableException>(Environment.GetEnvironmentVariable("VersionModelContainerName"), "VersionModelContainerName local variable is not set!");
				return Environment.GetEnvironmentVariable("VersionModelContainerName")!;
			}
		}

		/// <inheritdoc />
		/// <exception cref="LocalVariableException"/>
		public string WotDtoContainerName
		{
			get
			{
				Guard.NotNullOrEmpty<LocalVariableException>(Environment.GetEnvironmentVariable("WotDtoContainerName"), "WotDtoContainerName local variable is not set!");
				return Environment.GetEnvironmentVariable("WotDtoContainerName")!;
			}
		}
	}
}
