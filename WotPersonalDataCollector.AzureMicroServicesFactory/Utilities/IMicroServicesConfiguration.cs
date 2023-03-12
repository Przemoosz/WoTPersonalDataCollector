namespace WotPersonalDataCollector.AzureMicroServicesFactory.Utilities
{
	using WotPersonalDataCollector.SharedKernel.Utilities;

	/// <summary>
	/// Extends <see cref="IConfigurationBase"/> interface, with properties used in WotPersonalDataCollector.AzureMicroServicesFactory app.
	/// </summary>
	/// <inheritdoc />
	internal interface IMicroServicesConfiguration: IConfigurationBase
	{
		/// <summary>
		/// Gets value of admin username, necessary for creating access token.
		/// </summary>
		/// <value>
		/// <see cref="string"/> value of AdminUsername local variable.
		/// </value>
		string AdminUsername { get; }

		/// <summary>
		/// Gets value of admin password, necessary for creating access token.
		/// </summary>
		/// <value>
		/// <see cref="string"/> value of AdminPassword local variable.
		/// </value>
		string AdminPassword { get; }
	}
}
