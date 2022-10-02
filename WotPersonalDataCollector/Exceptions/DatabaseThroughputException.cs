using System;

namespace WotPersonalDataCollector.Exceptions
{
    internal class DatabaseThroughputException: Exception
    {
        public DatabaseThroughputException(): base()
        {
        }

        public DatabaseThroughputException(string message): base(message)
        {
        }

        public DatabaseThroughputException(string message, Exception innerException): base(message, innerException)
        {
        }
    }
}
