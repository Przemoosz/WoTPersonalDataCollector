namespace WotPersonalDataCollectorWebApp.Properties
{
	using System.Diagnostics.CodeAnalysis;

	/// <inheritdoc />
	[ExcludeFromCodeCoverage]
	public sealed class ResourcesWrapper: IResourcesWrapper
	{
		public string ValidationOperationIsAlreadyStartedMessage => Resources.ValidationOperationIsAlreadyStartedMessage;
		public string CancelingCancelledOperationMessage => Resources.CancelingCancelledOperationMessage;
		public string CancellationOperationHasAlreadyStaredMessage => Resources.CancellationOperationHasAlreadyStaredMessage;
		public string CancellationTokenDisposedMessage => Resources.CancellationTokenDisposedMessage;
		public string CancelingValidation => Resources.CancelingValidation;
	}
}
