using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuardNet;
using WotPersonalDataCollector.Exceptions;


namespace WotPersonalDataCollector.Utilities
{
    internal class Configuration: IConfiguration 
    {
        public string ApplicationId
        {
            get
            {
                Guard.NotNullOrEmpty<LocalVariableException>(Environment.GetEnvironmentVariable("ApplicationId"), "ApplicationId local variable is not set!");
                return Environment.GetEnvironmentVariable("ApplicationId");
            }
        }
        public string UserName
        {
            get
            {
                Guard.NotNullOrEmpty<LocalVariableException>(Environment.GetEnvironmentVariable("UserName"), "UserName local variable is not set!");
                return Environment.GetEnvironmentVariable("UserName");
            }
        }
    }
}
