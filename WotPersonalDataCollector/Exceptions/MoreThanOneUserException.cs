using System;

namespace WotPersonalDataCollector.Exceptions
{
    internal class MoreThanOneUserException: Exception
    {
        public MoreThanOneUserException(): base()
        {
        }

        public MoreThanOneUserException(string message): base(message)
        {
        }

        public MoreThanOneUserException(string message, Exception innerException): base(message, innerException)
        {
        }
    }
}
