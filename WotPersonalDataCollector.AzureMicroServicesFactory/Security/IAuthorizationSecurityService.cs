namespace WotPersonalDataCollector.AzureMicroServicesFactory.Security
{
	using System;

	/// <summary>
	/// Defines methods and properties for authorization security service. Service should protect api from
	/// repetitive call with wrong authorization token.
	/// </summary>

	internal interface IAuthorizationSecurityService
	{
		/// <summary>
		/// Gets number of total attempts.
		/// </summary>
		/// <value><see cref="int"/> value of total attempts.</value>
		public int TotalAttempts { get; }

		/// <summary>
		/// Gets number of total wrong attempts.
		/// </summary>
		/// <value><see cref="int"/> value of total wrong attempts.</value>
		public int TotalWrongAttempts { get; }

		/// <summary>
		/// Gets number of maximal wrong attempts before acquiring block. Value can not be changed in runtime.
		/// </summary>
		/// <value><see cref="int"/> value of maximal wrong attempts before acquiring block.</value>
		public int MaxWrongAttempts { get; }
		/// <summary>
		/// Gets <see cref="DateTime"/> value when block will expire.
		/// </summary>
		/// <value><see cref="DateTime"/> when block will expire.</value>
		public DateTime BlockExpireDateTime { get; }
		/// <summary>
		/// Gets whether authorization is not blocked. If authorization is blocked, methods tries to release block.
		/// </summary>
		/// <returns><see cref="bool"/> value whether authorization is not blocked.</returns>
		bool IsAuthorizationAvailable();
		/// <summary>
		/// Saves authorization security check. If <paramref name="isAuthorizedCorrectly"/> is false, adds value to <see cref="TotalWrongAttempts"/>
		/// and acquire block if this value is greater than <see cref="MaxWrongAttempts"/>.
		/// </summary>
		/// <param name="isAuthorizedCorrectly">Boolean value defines if request provided correct authorization token.</param>
		void SaveSecurityCheck(bool isAuthorizedCorrectly);
	}
}