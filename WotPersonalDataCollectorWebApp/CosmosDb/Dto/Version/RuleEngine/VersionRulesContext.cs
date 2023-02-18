namespace WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version.RuleEngine
{
	internal sealed class VersionRulesContext
	{
		public SemanticVersionModel AspVersionModel { get; init; }
		public SemanticVersionModel CosmosVersionModel { get; init; }
	}
}
