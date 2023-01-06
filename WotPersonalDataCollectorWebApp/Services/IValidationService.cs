namespace WotPersonalDataCollectorWebApp.Services;

public interface IValidationService
{
	/// <summary>
	/// Starts dto validation process
	/// </summary>
	/// <returns>A task that represents the asynchronous operation.</returns>
	Task RunValidationProcessAsync();
	/// <summary>
	/// Gets whether validation process has finished. If process was not started, it returns false.
	/// </summary>
	bool IsValidationFinished { get; }
}