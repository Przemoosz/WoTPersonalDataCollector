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

        public string WotDtoContainerName
        {
            get
            {
                Guard.NotNullOrEmpty<LocalVariableException>(Environment.GetEnvironmentVariable("WotDtoContainerName"), "WotDtoContainerName local variable is not set!");
                return Environment.GetEnvironmentVariable("WotDtoContainerName")!;
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

        public string WotDtoVersion
        {
            get
            {
                Guard.NotNullOrEmpty<LocalVariableException>(Environment.GetEnvironmentVariable("WotDtoVersion"), "WotDtoVersion local variable is not set!");
                return Environment.GetEnvironmentVariable("WotDtoVersion")!;
            }
        }

        public string VersionModelContainerName
        {
	        get
	        {
		        Guard.NotNullOrEmpty<LocalVariableException>(Environment.GetEnvironmentVariable("VersionModelContainerName"), "VersionModelContainerName local variable is not set!");
		        return Environment.GetEnvironmentVariable("VersionModelContainerName")!;
	        }
        }
	}
}