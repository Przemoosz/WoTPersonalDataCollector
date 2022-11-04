namespace WotPersonalDataCollectorWebApp.Services;

public interface IValidationCancellationService: IDisposable
{
	CancellationToken GetValidationCancellationToken();
	CancellationToken GetValidationCancellationToken(CancellationToken externalCancellationToken);
	void CancelValidation();
}