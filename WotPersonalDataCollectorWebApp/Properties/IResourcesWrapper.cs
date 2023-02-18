namespace WotPersonalDataCollectorWebApp.Properties;

/// <summary>
/// Wrapper for <see cref="Resources"/> class
/// </summary>
public interface IResourcesWrapper
{
	/// <summary>
	/// Gets <see cref="string"/> value of ValidationOperationIsAlreadyStartedMessage local resource. 
	/// </summary>
	/// <value>
	///	<see cref="string"/> value: "Validation Operation has already started, can't start another one. Please wait."
	/// </value>
	string ValidationOperationIsAlreadyStartedMessage { get;}

	/// <summary>
	/// Gets <see cref="string"/> value of CancelingCancelledOperationMessage local resource. 
	/// </summary>
	/// <value>
	/// <see cref="string"/> value: "Can not cancel operation that was not started or is finished!"
	/// </value>
	string CancelingCancelledOperationMessage { get; }

	/// <summary>
	/// Gets <see cref="string"/> value of CancellationOperationHasAlreadyStaredMessage local resource. 
	/// </summary>
	/// <value>
	/// <see cref="string"/> value: "Operation cancellation has already started."
	/// </value>
	string CancellationOperationHasAlreadyStaredMessage { get; }

	/// <summary>
	/// Gets <see cref="string"/> value of CancellationTokenDisposedMessage local resource. 
	/// </summary>
	/// <value>
	/// <see cref="string"/> value: "Cancellation token was disposed, that means validation operation is finished."
	/// </value>
	string CancellationTokenDisposedMessage { get; }

	/// <summary>
	/// Gets <see cref="string"/> value of CancelingValidation local resource. 
	/// </summary>
	/// <value>
	/// <see cref="string"/> value: "Canceling validation"
	/// </value>
	string CancelingValidation { get; }

	/// <summary>
	/// Gets <see cref="string"/> value of DataDeleteProcessStarted local resource. 
	/// </summary>
	/// <value>
	/// <see cref="string"/> value: "{0} delete process started"
	/// </value>
	string DataDeleteProcessStarted { get; }
}