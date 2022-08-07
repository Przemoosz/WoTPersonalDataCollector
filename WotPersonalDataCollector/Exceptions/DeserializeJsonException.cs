using System;

namespace WotPersonalDataCollector.Exceptions
{
    internal class DeserializeJsonException: Exception
    {
        public DeserializeJsonException()
        {
        }

        public DeserializeJsonException(string message) : base(message)
        {
        }

        public DeserializeJsonException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
