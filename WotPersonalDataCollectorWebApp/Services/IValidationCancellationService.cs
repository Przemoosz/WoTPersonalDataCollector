namespace WotPersonalDataCollectorWebApp.Services
{
	/// <summary>
	/// Service responsible for creating, maintaining and canceling <see cref="CancellationToken"/> for cosmos dto validation process.
	/// </summary>
	public interface IValidationCancellationService: IDisposable
	{
		/// <summary>
		/// Gets whether <see cref="CancellationToken"/> is initialized and available to use.
		/// </summary>
		/// <value>Boolean value if <see cref="CancellationToken"/> is available.</value>
		bool IsCancellationAvailable { get; }

		/// <summary>
		/// Gets whether <see cref="CancellationToken"/> is already requested.
		/// </summary>
		/// <value>Boolean value if <see cref="CancellationToken"/> is already requested.</value>
		bool IsCancellationRequested { get; }

		/// <summary>
		/// Gets whether <see cref="CancellationToken"/> was disposed or not initialized.
		/// </summary>
		/// <value>Boolean value if <see cref="CancellationToken"/> was disposed or not initialized.</value>
		bool IsTokenDisposed { get; }

		/// <summary>
		/// Creates new <see cref="CancellationTokenSource"/> (if not exists or disposed) and returns <see cref="CancellationToken"/>.
		/// </summary>
		/// <returns><see cref="CancellationToken"/> associated with instance of class, until its disposed.</returns>
		CancellationToken GetValidationCancellationToken();

		/// <summary>
		/// Creates new linked <see cref="CancellationTokenSource"/> (if not exists or disposed) and returns <see cref="CancellationToken"/>.
		/// </summary>
		/// <param name="externalCancellationToken">External <see cref="CancellationToken"/>.</param>
		/// <returns><see cref="CancellationToken"/> with linked external token, associated with instance of class, until its disposed.</returns>
		CancellationToken GetValidationCancellationToken(CancellationToken externalCancellationToken);

		/// <summary>
		/// Cancel <see cref="CancellationToken"/> associated with object.
		/// </summary>
		/// <exception cref="ValidationCancellationException"></exception>
		void CancelValidation();
	}
}
