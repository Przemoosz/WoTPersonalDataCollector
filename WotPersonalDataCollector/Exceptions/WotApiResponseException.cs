using System;

namespace WotPersonalDataCollector.Exceptions
{
    internal class WotApiResponseException: Exception
    {
        public WotApiResponseException()
        {
        }

        public WotApiResponseException(string message) : base(message)
        {
        }

        public WotApiResponseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
