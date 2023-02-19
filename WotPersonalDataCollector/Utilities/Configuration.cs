namespace WotPersonalDataCollector.Utilities
{
	using System;
	using GuardNet;
	using Exceptions;
	using WotPersonalDataCollector.SharedKernel.Utilities;
	using WotPersonalDataCollector.SharedKernel.Exceptions;

	/// <summary>
	/// Implementation of <see cref="IConfiguration"/> interface. Provides values of local variables.
	/// </summary>
	internal sealed class Configuration: ConfigurationBase, IConfiguration 
    {
	    /// <inheritdoc />
	    /// <exception cref="LocalVariableException"/>
		public string ApplicationId
        {
            get
            {
                Guard.NotNullOrEmpty<LocalVariableException>(Environment.GetEnvironmentVariable("ApplicationId"), "ApplicationId local variable is not set!");
                return Environment.GetEnvironmentVariable("ApplicationId");
            }
        }

	    /// <inheritdoc />
	    /// <exception cref="LocalVariableException"/>
		public string UserName
        {
            get
            {
                Guard.NotNullOrEmpty<LocalVariableException>(Environment.GetEnvironmentVariable("WotUserName"), "WotUserName local variable is not set!");
                return Environment.GetEnvironmentVariable("WotUserName");
            }
        }

	    /// <inheritdoc />
	    /// <exception cref="LocalVariableException"/>
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

	    /// <inheritdoc />
	    /// <exception cref="LocalVariableException"/>
		public string PlayersUri
        {
            get
            {
                Guard.NotNullOrEmpty<LocalVariableException>(Environment.GetEnvironmentVariable("PlayersUri"), "PlayersUri local variable is not set!");
                return Environment.GetEnvironmentVariable("PlayersUri");
            }
        }

	    /// <inheritdoc />
	    /// <exception cref="LocalVariableException"/>
		public string PersonalDataUri
        {
            get
            {
                Guard.NotNullOrEmpty<LocalVariableException>(Environment.GetEnvironmentVariable("PersonalDataUri"), "PersonalDataUri local variable is not set!");
                return Environment.GetEnvironmentVariable("PersonalDataUri");
            }
        }

		/// <inheritdoc />
		/// <exception cref="LocalVariableException"/>
		/// <exception cref="DatabaseThroughputException"/>
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
