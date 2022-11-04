﻿namespace WotPersonalDataCollectorWebApp.Services
{
	internal sealed class ValidationCancellationService: IValidationCancellationService
	{
		private readonly object _ctsCreateLock = new object();
		private readonly object _ctsCancelLock = new object();
		private CancellationTokenSource _validationCts;
		public CancellationToken GetValidationCancellationToken()
		{
			lock (_ctsCreateLock)
			{
				if (_validationCts is null)
				{
					_validationCts = new CancellationTokenSource();
					_validationCts.Token.Register(CancellationMessage);
				}
				return _validationCts.Token;
			}
		}

		public void CancelValidation()
		{
			lock (_ctsCancelLock)
			{
				if (_validationCts is null)
				{
					throw new Exception("not");
				}
				_validationCts.Cancel();
			}
		}

		public CancellationToken GetValidationCancellationToken(CancellationToken externalCancellationToken)
		{
			lock (_ctsCreateLock)
			{
				if (_validationCts is null)
				{
					_validationCts = CancellationTokenSource.CreateLinkedTokenSource(externalCancellationToken);
					_validationCts.Token.Register(CancellationMessage);
				}
				return _validationCts.Token;
			}
		}

		private void CancellationMessage()
		{
			Console.WriteLine($"Cancellation for validation requested by thread {Thread.CurrentThread.Name}");
		}

		public void Dispose()
		{
			_validationCts.Dispose();
		}
	}
}