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

        public string CosmosConnectionString
        {
            get
            {
                Guard.NotNullOrEmpty<LocalVariableException>(Environment.GetEnvironmentVariable("CosmosConnectionString"), "CosmosConnectionString local variable is not set!");
                return Environment.GetEnvironmentVariable("CosmosConnectionString");
            }
        }

        public string CosmosDbName
        {
            get
            {
                Guard.NotNullOrEmpty<LocalVariableException>(Environment.GetEnvironmentVariable("CosmosDbName"), "CosmosDbName local variable is not set!");
                return Environment.GetEnvironmentVariable("CosmosDbName");
            }
        }

        public int DatabaseThroughput
        {
            get
            {
                Guard.NotNullOrEmpty<LocalVariableException>(Environment.GetEnvironmentVariable("DatabaseThroughput"), "DatabaseThroughput local variable is not set!");
                if (!Int32.TryParse(Environment.GetEnvironmentVariable("DatabaseThroughput"), out int throughput))
                {
                    throw new DatabaseThroughputException(
                        "Can not parse provided throughput in local variables to Int32!");
                }

                if (throughput < 400)
                {
                    throw new DatabaseThroughputException(
                        "DatabaseThroughput can not be lower than 400!");
                }
                return throughput;
            }
        }

        public string ContainerName
        {
            get
            {
                Guard.NotNullOrEmpty<LocalVariableException>(Environment.GetEnvironmentVariable("ContainerName"), "ContainerName local variable is not set!");
                return Environment.GetEnvironmentVariable("ContainerName");
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
