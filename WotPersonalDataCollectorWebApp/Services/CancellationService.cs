using WotPersonalDataCollectorWebApp.Exceptions;

namespace WotPersonalDataCollectorWebApp.Services
{
	internal sealed class ValidationCancellationService: IValidationCancellationService
	{
		private readonly object _ctsCreateAndGetLock = new object();
		private readonly object _ctsCancelLock = new object();
		private CancellationTokenSource _validationCts;

		public bool IsCancellationRequested => _validationCts is not null && _validationCts.IsCancellationRequested;

		public bool IsCancellationAvailable
		{
			get
			{
				lock (_ctsCreateAndGetLock)
				{
					if (_validationCts is null)
					{
						return false;
					}
					try
					{
						var token = _validationCts!.Token;
					}
					catch (ObjectDisposedException)
					{
						return false;
					}
					return true;
				}
			}
		}

		public CancellationToken GetValidationCancellationToken()
		{
			lock (_ctsCreateAndGetLock)
			{
				if (_validationCts is not null)
				{
					return _validationCts.Token;
				}
				_validationCts = new CancellationTokenSource();
				_validationCts.Token.Register(CancellationMessage);
				return _validationCts.Token;
			}
		}

		public void CancelValidation()
		{
			lock (_ctsCancelLock)
			{
				if (_validationCts is null)
				{
					throw new ValidationCancellationException("Can not cancel operation that was not initialized - CancellationTokenService was not called");
				}
				_validationCts.Cancel();
			}
		}

		public CancellationToken GetValidationCancellationToken(CancellationToken externalCancellationToken)
		{
			lock (_ctsCreateAndGetLock)
			{
				if (_validationCts is not null)
				{
					return _validationCts.Token;
				}
				_validationCts = CancellationTokenSource.CreateLinkedTokenSource(externalCancellationToken);
				_validationCts.Token.Register(CancellationMessage);
				return _validationCts.Token;
			}
		}

		private void CancellationMessage()
		{
			Console.WriteLine($"Cancellation for validation requested by thread {Thread.CurrentThread.Name}");
		}

		public void Dispose()
		{
			if (_validationCts is not null)
			{
				_validationCts.Dispose();
			}
		}
	}
}
