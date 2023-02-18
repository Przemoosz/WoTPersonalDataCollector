using WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version.RuleEngine.Rules;

namespace WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version.RuleEngine;

internal interface IVersionRuleEngine
{
	void Validate(VersionRulesContext context, IEnumerable<IVersionRule> rules);
}