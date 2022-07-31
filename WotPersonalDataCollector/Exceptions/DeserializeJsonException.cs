using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
