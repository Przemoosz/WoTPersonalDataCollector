using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WotPersonalDataCollector.Exceptions
{
    internal class LocalVariableException: Exception
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
