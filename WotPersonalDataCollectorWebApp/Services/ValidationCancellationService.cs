using WotPersonalDataCollector.WebApp.Exceptions;

namespace WotPersonalDataCollector.WebApp.Services
{
	/// <inheritdoc />
	internal sealed class ValidationCancellationService: IValidationCancellationService
	{
		private readonly object _ctsCreateAndGetLock = new object();
		private readonly object _ctsCancelLock = new object();
		private readonly object _ctsDisposeLock = new object();
		private CancellationTokenSource _validationCts;
		private CancellationTokenRegistration _cancellationTokenRegistration;

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

		public bool IsTokenDisposed
		{
			get
			{
				lock (_ctsDisposeLock)
				{
					if (_validationCts is null)
					{
						return true;
					}
					try
					{
						var _ = _validationCts!.Token;
						return false;
					}
					catch (ObjectDisposedException)
					{
						return true;
					}
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
				if (IsTokenDisposed)
				{
					RegisterCancellationToken();
					return _validationCts!.Token;
				}
				return _validationCts!.Token;
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
				if (IsTokenDisposed)
				{
					RegisterCancellationToken(externalCancellationToken);
					return _validationCts!.Token;
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
					throw new ValidationCancellationException(
						"Can not cancel operation that was not initialized - CancellationTokenService was not called");
				}
				_validationCts.Cancel();
			}
		}

		public void Dispose()
		{
			lock (_ctsCancelLock)
			{
				if (_validationCts is not null)
				{
					_cancellationTokenRegistration.Dispose();
					_validationCts.Dispose();
				}
			}
		}

		private void CancellationMessage()
		{
			Console.WriteLine($"Cancellation for validation requested by thread {Thread.CurrentThread.Name}");
		}

		private void RegisterCancellationToken()
		{
			_validationCts = new CancellationTokenSource();
			_cancellationTokenRegistration = _validationCts.Token.Register(CancellationMessage);
		}

		private void RegisterCancellationToken(CancellationToken cancellationToken)
		{
			_validationCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
			_cancellationTokenRegistration = _validationCts.Token.Register(CancellationMessage);
		}
	}
}
