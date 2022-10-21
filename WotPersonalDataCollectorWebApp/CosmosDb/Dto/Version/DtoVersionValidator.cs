using WotPersonalDataCollectorWebApp.Exceptions;

namespace WotPersonalDataCollectorWebApp.CosmosDb.Dto.Version
{
    using Utilities;

    internal class DtoVersionValidator : IDtoVersionValidator
    {
        private readonly IAspConfiguration _aspConfiguration;
        private readonly ISemanticVersionModelFactory _semanticVersionModelFactory;
        private readonly ILogger _logger;

        public DtoVersionValidator(IAspConfiguration aspConfiguration, ISemanticVersionModelFactory semanticVersionModelFactory, ILogger logger)
        {
            _aspConfiguration = aspConfiguration;
            _semanticVersionModelFactory = semanticVersionModelFactory;
            _logger = logger;
        }

        public void EnsureVersionCorrectness(UserPersonalData userPersonalData)
        {
            SemanticVersionModel webSemanticVersionModel = _semanticVersionModelFactory.Create(_aspConfiguration.WotDtoVersion);
            SemanticVersionModel cosmosSemanticVersionModel = _semanticVersionModelFactory.Create(userPersonalData.ClassProperties.DtoVersion);
            if (webSemanticVersionModel.Major < cosmosSemanticVersionModel.Major)
            {
                //Tested
                _logger.LogError("CosmosDb have higher version of dto than this used in ASP.NET!");
                throw new DtoVersionException($"Version used in CosmosDb have higher Major number than Dto version used in ASP.NET app. Update ASP.NET! \n Cosmos: {cosmosSemanticVersionModel.Major} \n ASP.NET: {webSemanticVersionModel.Major}");
            }

            if (webSemanticVersionModel.Major > cosmosSemanticVersionModel.Major)
            {
                //Tested
	            _logger.LogError("CosmosDb have lower major version number than this used in ASP.NET!");
                throw new DtoVersionException($"Version used in CosmosDb have lower Major number than Dto version used in ASP.NET app. Update CosmosDto! \n Cosmos: {cosmosSemanticVersionModel.Major} \n ASP.NET: {webSemanticVersionModel.Major}");
            }

            if (webSemanticVersionModel.Minor > cosmosSemanticVersionModel.Minor)
            {
                _logger.LogError("CosmosDb have older version of dto than this used in ASP.NET!");
                throw new DtoVersionException($"Version used in CosmosDb have lower Minor number than Dto version used in ASP.NET app. Update CosmosDto! \n Cosmos: {cosmosSemanticVersionModel.Minor} \n ASP.NET: {webSemanticVersionModel.Minor}");
            }

            if (webSemanticVersionModel.Minor < cosmosSemanticVersionModel.Minor)
            {
                _logger.LogWarning($"CosmosDb Dto have higher minor version number than this used in ASP.NET. Consider updating dto, some feature will not be available. CosmosDb: {cosmosSemanticVersionModel}");
            }
            else if (webSemanticVersionModel.Patch < cosmosSemanticVersionModel.Patch)
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
