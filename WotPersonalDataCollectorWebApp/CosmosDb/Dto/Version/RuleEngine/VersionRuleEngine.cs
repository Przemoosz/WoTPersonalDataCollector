using WotPersonalDataCollectorWebApp.CosmosDb.Dto.Version.RuleEngine.Rules;

namespace WotPersonalDataCollectorWebApp.CosmosDb.Dto.Version.RuleEngine
{
	internal sealed class VersionRuleEngine: IVersionRuleEngine
	{
		public void Validate(VersionRulesContext context, IEnumerable<IVersionRule> rules)
		{
			foreach (IVersionRule rule in rules)
			{
				if (rule.CanEvaluateRule(context))
				{
					rule.Evaluate(context);
				}
			}
		}
	}
}
