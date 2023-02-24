using System.Diagnostics.CodeAnalysis;

namespace WotPersonalDataCollector.WebApp.Exceptions
{
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
