namespace WotPersonalDataCollector.Utilities
{
	using WotPersonalDataCollector.SharedKernel.Utilities;

	/// <summary>
	/// Extends <see cref="IConfigurationBase"/> interface, with properties used in WotPersonalDataCollector app.
	/// </summary>
	/// <inheritdoc />
	internal interface IConfiguration : IConfigurationBase
	{
		/// <summary>
		/// Gets value of application id, necessary for wot api connection.
		/// </summary>
		/// <value>
		/// <see cref="string"/> value of ApplicationId local variable.
		/// </value>
		public string ApplicationId { get; }

		/// <summary>
		/// Gets or sets value of user Id.
		/// </summary>
		/// <value>
		/// <see cref="string"/> value of UserId local variable.
		/// </value>
		public string UserId { get; set; }

		/// <summary>
		/// Gets value of wot username.
		/// </summary>
		/// <value>
		/// <see cref="string"/> value of UserName local variable.
		/// </value>
		public string UserName { get; }

		/// <summary>
		/// Gets value of personal data uri endpoint.
		/// </summary>
		/// <value>
		/// <see cref="string"/> value of PersonalDataUri local variable.
		/// </value>
		public string PersonalDataUri { get; }

		/// <summary>
		/// Gets value of players uri endpoint.
		/// </summary>
		/// <value>
		/// <see cref="string"/> value of PlayersUri local variable.
		/// </value>
		public string PlayersUri { get; }

		/// <summary>
		/// Gets value of database throughput.
		/// </summary>
		/// <value>
		/// <see cref="int"/> value of DatabaseThroughput local variable.
		/// </value>
		public int DatabaseThroughput { get; }

		/// <summary>
		/// Tries get UserNameLocalVariable.
		/// </summary>
		/// <param name="userName">Out value for UserName local variable.</param>
		/// <returns>True if UserName local variable is set. Otherwise False.</returns>
		bool TryGetUserName(out string userName);
	}
}

