namespace WotPersonalDataCollectorWebApp.Exceptions
{
	using System.Diagnostics.CodeAnalysis;

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
