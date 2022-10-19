using WotPersonalDataCollectorWebApp.Exceptions;

namespace WotPersonalDataCollectorWebApp.CosmosDb.Dto.Version
{
    using Utilities;

    internal class DtoVersionValidator : IDtoVersionValidator
    {
        private readonly IAspConfiguration _aspConfiguration;
        private readonly IDtoVersionFactory _dtoVersionFactory;
        private readonly ILogger _logger;

        public DtoVersionValidator(IAspConfiguration aspConfiguration, IDtoVersionFactory dtoVersionFactory, ILogger logger)
        {
            _aspConfiguration = aspConfiguration;
            _dtoVersionFactory = dtoVersionFactory;
            _logger = logger;
        }

        public void EnsureVersionCorrectness(UserPersonalData userPersonalData)
        {
            DtoVersion webDtoVersion = _dtoVersionFactory.Create(_aspConfiguration.WotDtoVersion);
            DtoVersion cosmosDtoVersion = _dtoVersionFactory.Create(userPersonalData.ClassProperties.DtoVersion);
            if (webDtoVersion.Major < cosmosDtoVersion.Major)
            {
                //Tested
                _logger.LogError("CosmosDb have higher version of dto than this used in ASP.NET!");
                throw new DtoVersionException($"Version used in CosmosDb have higher Major number than Dto version used in ASP.NET app. Update ASP.NET! \n Cosmos: {cosmosDtoVersion.Major} \n ASP.NET: {webDtoVersion.Major}");
            }

            if (webDtoVersion.Major > cosmosDtoVersion.Major)
            {
                //Tested
	            _logger.LogError("CosmosDb have lower major version number than this used in ASP.NET!");
                throw new DtoVersionException($"Version used in CosmosDb have lower Major number than Dto version used in ASP.NET app. Update CosmosDto! \n Cosmos: {cosmosDtoVersion.Major} \n ASP.NET: {webDtoVersion.Major}");
            }

            if (webDtoVersion.Minor > cosmosDtoVersion.Minor)
            {
                _logger.LogError("CosmosDb have older version of dto than this used in ASP.NET!");
                throw new DtoVersionException($"Version used in CosmosDb have lower Minor number than Dto version used in ASP.NET app. Update CosmosDto! \n Cosmos: {cosmosDtoVersion.Minor} \n ASP.NET: {webDtoVersion.Minor}");
            }

            if (webDtoVersion.Minor < cosmosDtoVersion.Minor)
            {
                _logger.LogWarning($"CosmosDb Dto have higher minor version number than this used in ASP.NET. Consider updating dto, some feature will not be available. CosmosDb: {cosmosDtoVersion}");
            }
            else if (webDtoVersion.Patch < cosmosDtoVersion.Patch)
            {
                _logger.LogWarning("CosmosDb have have higher patch version number than this used in ASP.NET. Consider updating dto, some features could be broken!");
            }
            else
            {
                // Tested
                _logger.LogInformation("ASP.NET Dto version matches CosmosDb Dto Version");
            }

        }

    }
}
