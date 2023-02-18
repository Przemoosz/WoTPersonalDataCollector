namespace WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version.RuleEngine.Rules
{
	internal sealed class HigherAspPatchVersionRule : IVersionRule
	{
		private readonly ILogger _logger;

		public HigherAspPatchVersionRule(ILogger logger)
		{
			_logger = logger;
		}

		public void Evaluate(VersionRulesContext context)
		{
			if (context.AspVersionModel.Patch > context.CosmosVersionModel.Patch)
			{
				_logger.LogWarning("CosmosDb have have lower patch version number than this used in ASP.NET. CosmosDb Dto is outdated!");
			}
		}

		public bool CanEvaluateRule(VersionRulesContext context)
		{
			return context.AspVersionModel.Major == context.CosmosVersionModel.Major &&
			       context.AspVersionModel.Minor <= context.CosmosVersionModel.Minor;
		}
	}
}
