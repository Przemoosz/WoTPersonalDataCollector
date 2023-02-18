namespace WotPersonalDataCollectorWebApp.Exceptions
{
	using System.Diagnostics.CodeAnalysis;

	[ExcludeFromCodeCoverage]
	public class ValidationCancellationException: Exception
	{
		public ValidationCancellationException(): base()
		{
		}

		public ValidationCancellationException(string message): base(message)
		{
		}

		public ValidationCancellationException(string exception, Exception innerException): base(exception, innerException)
		{
		}
	}
}
