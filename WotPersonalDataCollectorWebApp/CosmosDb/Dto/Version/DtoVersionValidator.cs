namespace WotPersonalDataCollectorWebApp.CosmosDb.Dto.Version
{
    using Utilities;
    using RuleEngine;
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
            SemanticVersionModel aspVersionModel = _semanticVersionModelFactory.Create(_aspConfiguration.WotDtoVersion);
            SemanticVersionModel cosmosVersionModel = _semanticVersionModelFactory.Create(userPersonalData.ClassProperties.DtoVersion);
            var context = new VersionRulesContext()
            {
	            AspVersionModel = aspVersionModel,
	            CosmosVersionModel = cosmosVersionModel
            };

        }
    }
}
