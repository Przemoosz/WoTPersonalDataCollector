namespace WotPersonalDataCollector.SharedKernel.Utilities
{
	/// <summary>
	/// Defines base configuration, that works with local variables.
	/// </summary>
	public interface IConfigurationBase
	{
		/// <summary>
		/// Gets value of cosmos database connection string.
		/// </summary>
		/// <value>
		/// <see cref="string"/> value of CosmosConnectionString local variable.
		/// </value>
		string CosmosConnectionString { get; }

		/// <summary>
		/// Gets value of cosmos database name.
		/// </summary>
		/// <value>
		/// <see cref="string"/> value of DatabaseName local variable.
		/// </value>
		string DatabaseName { get; }

		/// <summary>
		/// Gets value of wot dto version.
		/// </summary>
		/// <value>
		/// <see cref="string"/> value of WotDtoVersion local variable.
		/// </value>
		string WotDtoVersion { get; }

		/// <summary>
		/// Gets value of wot dto container name.
		/// </summary>
		/// <value>
		/// <see cref="string"/> value of WotDtoContainerName local variable.
		/// </value>
		string WotDtoContainerName { get; }

		/// <summary>
		/// Gets value of version model container name.
		/// </summary>
		/// <value>
		/// <see cref="string"/> value of VersionModelContainerName local variable.
		/// </value>
		string VersionModelContainerName { get; }
	}
}

