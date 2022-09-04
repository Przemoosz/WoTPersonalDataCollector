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

        public string UserId
        {
            get
            {
                Guard.NotNullOrEmpty<LocalVariableException>(Environment.GetEnvironmentVariable("UserId"), "UserId local variable is not set!");
                return Environment.GetEnvironmentVariable("UserId");
            }
            set
            {
                Guard.NotNullOrEmpty<LocalVariableException>(value, "Provided userId can not be null or empty value!");
                Environment.SetEnvironmentVariable("UserId", value);
            }
        }

        public string PlayersUri
        {
            get
            {
                Guard.NotNullOrEmpty<LocalVariableException>(Environment.GetEnvironmentVariable("PlayersUri"), "PlayersUri local variable is not set!");
                return Environment.GetEnvironmentVariable("PlayersUri");
            }
        }

        public string PersonalDataUri
        {
            get
            {
                Guard.NotNullOrEmpty<LocalVariableException>(Environment.GetEnvironmentVariable("PersonalDataUri"), "PersonalDataUri local variable is not set!");
                return Environment.GetEnvironmentVariable("PersonalDataUri");
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
