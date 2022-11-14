namespace WotPersonalDataCollectorWebApp.Services;

public interface IValidationCancellationService: IDisposable
{
	bool IsCancellationAvailable { get; }
	bool IsCancellationRequested { get; }
	CancellationToken GetValidationCancellationToken();
	CancellationToken GetValidationCancellationToken(CancellationToken externalCancellationToken);
	void CancelValidation();
}