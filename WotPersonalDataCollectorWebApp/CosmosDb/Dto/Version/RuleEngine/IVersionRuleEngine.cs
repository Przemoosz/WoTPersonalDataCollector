namespace WotPersonalDataCollectorWebApp.CosmosDb.Dto.Version.RuleEngine;

internal interface IVersionRuleEngine
{
	void Validate(VersionRulesContext context);
}