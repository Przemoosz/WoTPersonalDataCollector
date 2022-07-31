using System;
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
                Guard.NotNullOrEmpty<LocalVariableException>(Environment.GetEnvironmentVariable("WotUserName"), "WotUserName local variable is not set!");
                return Environment.GetEnvironmentVariable("WotUserName");
            }
        }

        public bool TryGetUserName(out string userName)
        {
            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WotUserName")))
            {
                userName = Environment.GetEnvironmentVariable("WotUserName");
                return true;
            }
            userName = null;
            return false;
        }
    }
}
