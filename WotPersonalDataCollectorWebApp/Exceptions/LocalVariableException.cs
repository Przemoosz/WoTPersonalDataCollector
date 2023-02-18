using System.Diagnostics.CodeAnalysis;

namespace WotPersonalDataCollector.WebApp.Exceptions
{
	[ExcludeFromCodeCoverage]
	internal class LocalVariableException : Exception
    {
        public LocalVariableException()
        {
        }

        public LocalVariableException(string message) : base(message)
        {
        }

        public LocalVariableException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
