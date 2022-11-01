namespace WotPersonalDataCollectorWebApp.CosmosDb.Dto.Version.RuleEngine.Rules;

internal interface IVersionRule
{
	void Evaluate(VersionRulesContext context);
	bool CanEvaluateRule(VersionRulesContext context);
}