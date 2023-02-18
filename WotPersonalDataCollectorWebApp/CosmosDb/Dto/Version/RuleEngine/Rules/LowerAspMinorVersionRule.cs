namespace WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version.RuleEngine.Rules
{
	internal sealed class LowerAspMinorVersionRule: IVersionRule
	{
		private readonly ILogger _logger;

		public LowerAspMinorVersionRule(ILogger logger)
		{
			_logger = logger;
		}

		public void Evaluate(VersionRulesContext context)
		{
			if (context.AspVersionModel.Minor < context.CosmosVersionModel.Minor)
			{
				_logger.LogWarning($"CosmosDb Dto have higher minor version number than this used in ASP.NET. Consider updating dto, some feature will not be available. \n CosmosDb: Cosmos: {context.CosmosVersionModel.Minor} \n ASP.NET: {context.AspVersionModel.Minor}");
			}
		}

		public bool CanEvaluateRule(VersionRulesContext context) => context.AspVersionModel.Major == context.CosmosVersionModel.Major;
	}
}
