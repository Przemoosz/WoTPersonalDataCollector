namespace WotPersonalDataCollectorWebApp.Services;

/// <summary>
/// Service responsible for validation versions of objects <see cref="T:WotPersonalDataCollectorWebApp.CosmosDb.Dto.WotDataCosmosDbDto"/> in asp.net and cosmos db.
/// </summary>
public interface IValidationService
{
	/// <summary>
	/// Gets whether validation process has finished. If process was not started, it returns false.
	/// </summary>
	/// <value>Boolean value if process has finished</value>
	bool IsValidationFinished { get; }

	/// <summary>
	/// Starts dto validation process.
	/// </summary>
	/// <returns>A task that represents the asynchronous operation.</returns>
	Task RunValidationProcessAsync();

}