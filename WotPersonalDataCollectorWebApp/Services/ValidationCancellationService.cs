namespace WotPersonalDataCollectorWebApp.Services
{
	using Exceptions;

	internal sealed class ValidationCancellationService: IValidationCancellationService
	{
		private readonly object _ctsCreateAndGetLock = new object();
		private readonly object _ctsCancelLock = new object();
		private CancellationTokenSource _validationCts;

		public bool IsCancellationRequested
		{
			get
			{
				lock (_ctsCreateAndGetLock)
				{
					return _validationCts is not null && _validationCts.IsCancellationRequested;
				}
			}
		}

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
						var _ = _validationCts!.Token;
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
				if (_validationCts is null)
				{
					RegisterCancellationToken();
				}
				return _validationCts!.Token;
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
				if (_validationCts is null)
				{
					RegisterCancellationToken(externalCancellationToken);
				}
				return _validationCts!.Token;
			}
		}

		public void Dispose()
		{
			if (_validationCts is not null)
			{
				_validationCts.Dispose();
			}
		}

		private void CancellationMessage()
		{
			Console.WriteLine($"Cancellation for validation requested by thread {Thread.CurrentThread.Name}");
		}

		private void RegisterCancellationToken()
		{
			_validationCts = new CancellationTokenSource();
			_validationCts.Token.Register(CancellationMessage);
		}

		private void RegisterCancellationToken(CancellationToken cancellationToken)
		{
			_validationCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
			_validationCts.Token.Register(CancellationMessage);
		}
	}
}
