namespace WotPersonalDataCollectorWebApp.CosmosDb.Dto.Version.RuleEngine.Rules
{
	internal sealed class AspVersionEqualsCosmosVersionRule: IVersionRule
	{
		private readonly ILogger _logger;

		public AspVersionEqualsCosmosVersionRule(ILogger logger)
		{
			_logger = logger;
		}
		public void Evaluate(VersionRulesContext context)
		{
			_logger.LogInformation("ASP.NET Dto version matches CosmosDb Dto Version");
		}

		public bool CanEvaluateRule(VersionRulesContext context)
		{
			return context.AspVersionModel.Major == context.CosmosVersionModel.Major &&
			       context.AspVersionModel.Minor == context.CosmosVersionModel.Minor &&
			       context.AspVersionModel.Patch == context.CosmosVersionModel.Patch;
		}
	}
}
