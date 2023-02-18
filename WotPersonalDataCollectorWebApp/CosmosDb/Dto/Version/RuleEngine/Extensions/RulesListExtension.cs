using WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version.RuleEngine.Rules;

namespace WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version.RuleEngine.Extensions
{
	internal static class RulesListExtension
    {
        public static List<T> AddRules<T>(this List<T> list, params T[] rules) where T : IVersionRule
        {
            list.AddRange(rules);
            return list;
        }
    }
}
