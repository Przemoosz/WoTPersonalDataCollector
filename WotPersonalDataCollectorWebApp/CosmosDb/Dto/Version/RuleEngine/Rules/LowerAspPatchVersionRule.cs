namespace WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version.RuleEngine.Rules
{
	internal sealed class LowerAspPatchVersionRule:IVersionRule
	{
		private readonly ILogger _logger;

		public LowerAspPatchVersionRule(ILogger logger)
		{
			_logger = logger;
		}

		public void Evaluate(VersionRulesContext context)
		{
			if (context.AspVersionModel.Patch < context.CosmosVersionModel.Patch)
			{
				_logger.LogWarning("CosmosDb have have higher patch version number than this used in ASP.NET. Consider updating dto, some features could be broken!");
			}
		}

		public bool CanEvaluateRule(VersionRulesContext context)
		{
			return context.AspVersionModel.Major == context.CosmosVersionModel.Major &&
			       context.AspVersionModel.Minor <= context.CosmosVersionModel.Minor;
		}
	}
}
