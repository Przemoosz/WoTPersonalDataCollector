using GuardNet;
using WotPersonalDataCollectorWebApp.Exceptions;

namespace WotPersonalDataCollectorWebApp.Utilities
{
    internal sealed class AspConfiguration : IAspConfiguration
    {
        public string DatabaseName
        {
            get
            {
                Guard.NotNullOrEmpty<LocalVariableException>(Environment.GetEnvironmentVariable("DatabaseName"), "DatabaseName local variable is not set!");
                return Environment.GetEnvironmentVariable("DatabaseName")!;
            }
        }

        public string ContainerName
        {
            get
            {
                Guard.NotNullOrEmpty<LocalVariableException>(Environment.GetEnvironmentVariable("ContainerName"), "ContainerName local variable is not set!");
                return Environment.GetEnvironmentVariable("ContainerName")!;
            }
        }

        public string CosmosConnectionString
        {
            get
            {
                Guard.NotNullOrEmpty<LocalVariableException>(Environment.GetEnvironmentVariable("CosmosConnectionString"), "CosmosConnectionString local variable is not set!");
                var connectionString = Environment.GetEnvironmentVariable("CosmosConnectionString")!;
                if (connectionString.Length != 139)
                {
                    throw new LocalVariableException(
                        "Connection string length is not valid. 139 chars connection string is required!");
                }
                return connectionString;
            }
        }
    }
}