namespace WotPersonalDataCollectorWebApp.CosmosDb.Dto.Version.RuleEngine.Extensions
{
    using Rules;
    internal static class RulesListExtension
    {
        public static List<T> AddRules<T>(this List<T> list, params T[] rules) where T : IVersionRule
        {
            list.AddRange(rules);
            return list;
        }
    }
}
