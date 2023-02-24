using System.Diagnostics.CodeAnalysis;

namespace WotPersonalDataCollector.WebApp.Exceptions
{
	[ExcludeFromCodeCoverage]
	public class DtoVersionException: Exception
    {
        public DtoVersionException()
        {
        }

        public DtoVersionException(string message): base(message)
        {
        }

        public DtoVersionException(string message, Exception innerException): base(message, innerException)
        {
        }
    }
}
