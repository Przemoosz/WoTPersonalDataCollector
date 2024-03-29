﻿using System.Diagnostics.CodeAnalysis;

namespace WotPersonalDataCollector.WebApp.Properties
{
	/// <inheritdoc />
	[ExcludeFromCodeCoverage]
	public sealed class ResourcesWrapper: IResourcesWrapper
	{
		public string ValidationOperationIsAlreadyStartedMessage => Resources.ValidationOperationIsAlreadyStartedMessage;
		public string CancelingCancelledOperationMessage => Resources.CancelingCancelledOperationMessage;
		public string CancellationOperationHasAlreadyStaredMessage => Resources.CancellationOperationHasAlreadyStaredMessage;
		public string CancellationTokenDisposedMessage => Resources.CancellationTokenDisposedMessage;
		public string CancelingValidation => Resources.CancelingValidation;
		public string DataDeleteProcessStarted => Resources.DataDeleteProcessStarted;
	}
}
