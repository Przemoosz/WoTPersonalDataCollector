namespace WotPersonalDataCollectorWebApp.Services
{
	/// <summary>
	/// Service responsible for creating, maintaining and canceling cancellation token for cosmos dto validation process.
	/// </summary>
	public interface IValidationCancellationService: IDisposable
	{
		/// <summary>
		/// Gets whether cancellation token is initialized and available to use.
		/// </summary>
		bool IsCancellationAvailable { get; }
		/// <summary>
		/// Gets whether cancellation token is already requested.
		/// </summary>
		bool IsCancellationRequested { get; }
		/// <summary>
		/// Gets whether cancellation token was disposed or not initialized.
		/// </summary>
		bool IsTokenDisposed { get; }
		/// <summary>
		/// Creates new CancellationTokenSource (if not exists or disposed) and returns CancellationToken.
		/// </summary>
		/// <returns>CancellationToken associated with instance of class, until its disposed.</returns>
		CancellationToken GetValidationCancellationToken();
		/// <summary>
		/// Creates new linked CancellationTokenSource (if not exists or disposed) and returns CancellationToken.
		/// </summary>
		/// <param name="externalCancellationToken">External CancellationToken.</param>
		/// <returns>CancellationToken associated with instance of class, with linked external token, until its disposed.</returns>
		CancellationToken GetValidationCancellationToken(CancellationToken externalCancellationToken);
		/// <summary>
		/// Cancel cancellation token associated with object.
		/// </summary>
		/// <exception cref="ValidationCancellationException"></exception>
		void CancelValidation();
	}
}
