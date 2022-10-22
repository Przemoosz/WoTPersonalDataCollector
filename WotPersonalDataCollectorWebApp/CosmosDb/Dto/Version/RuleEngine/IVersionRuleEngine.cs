using WotPersonalDataCollectorWebApp.CosmosDb.Dto.Version.RuleEngine.Rules;

namespace WotPersonalDataCollectorWebApp.CosmosDb.Dto.Version.RuleEngine;

internal interface IVersionRuleEngine
{
	void Validate(VersionRulesContext context, IEnumerable<IVersionRule> rules);
}