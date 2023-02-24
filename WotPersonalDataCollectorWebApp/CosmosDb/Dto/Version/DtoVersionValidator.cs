using WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version.RuleEngine;
using WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version.RuleEngine.Extensions;
using WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version.RuleEngine.Factory;
using WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version.RuleEngine.Rules;
using WotPersonalDataCollector.WebApp.Utilities;

namespace WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version
{
	internal class DtoVersionValidator : IDtoVersionValidator
    {
        private readonly IAspConfiguration _aspConfiguration;
        private readonly ISemanticVersionModelFactory _semanticVersionModelFactory;
        private readonly IRulesFactory _rulesFactory;
        private readonly IVersionRuleEngine _versionRuleEngine;

        public DtoVersionValidator(IAspConfiguration aspConfiguration,
	        ISemanticVersionModelFactory semanticVersionModelFactory, IRulesFactory rulesFactory,
	        IVersionRuleEngine versionRuleEngine)
        {
            _aspConfiguration = aspConfiguration;
            _semanticVersionModelFactory = semanticVersionModelFactory;
            _rulesFactory = rulesFactory;
            _versionRuleEngine = versionRuleEngine;
        }

        public void EnsureVersionCorrectness(WotDataCosmosDbDto wotDataCosmosDbDto)
        {
            SemanticVersionModel aspVersionModel = _semanticVersionModelFactory.Create(_aspConfiguration.WotDtoVersion);
            SemanticVersionModel cosmosVersionModel = _semanticVersionModelFactory.Create(wotDataCosmosDbDto.ClassProperties.DtoVersion);
            var context = new VersionRulesContext()
            {
	            AspVersionModel = aspVersionModel,
	            CosmosVersionModel = cosmosVersionModel
            };
            List<IVersionRule> rules = new List<IVersionRule>(10);
            rules.AddRules(
	            _rulesFactory.CreateHigherAspMajorVersionRule(),
	            _rulesFactory.CreateLowerAspMajorVersionRule(),
	            _rulesFactory.CreateHigherAspMinorVersionRule(),
	            _rulesFactory.CreateLowerAspMinorVersionRule(),
	            _rulesFactory.CreateHigherAspPatchVersionRule(),
	            _rulesFactory.CreateLowerAspPatchVersionRule(),
	            _rulesFactory.CreateAspVersionEqualsCosmosVersionRule());
            _versionRuleEngine.Validate(context,rules);
        }
    }
}
