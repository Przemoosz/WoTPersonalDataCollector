using WotPersonalDataCollectorWebApp.CosmosDb.Dto.Version.RuleEngine.Rules;

namespace WotPersonalDataCollectorWebApp.CosmosDb.Dto.Version.RuleEngine
{
	internal sealed class VersionRuleEngine: IVersionRuleEngine
	{
		private readonly IEnumerable<IVersionRule> _rules;

		public VersionRuleEngine(IEnumerable<IVersionRule> rules)
		{
			_rules = rules;
		}

		public void Validate(VersionRulesContext context)
		{
			foreach (IVersionRule rule in _rules)
			{
				if (rule.CanEvaluateRule(context))
				{
					rule.Evaluate(context);
				}
			}
		}
	}
}
