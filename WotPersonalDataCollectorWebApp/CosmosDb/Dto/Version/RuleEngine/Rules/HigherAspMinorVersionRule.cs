using WotPersonalDataCollector.WebApp.Exceptions;

namespace WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version.RuleEngine.Rules
{
	internal sealed class HigherAspMinorVersionRule: IVersionRule
	{
		private readonly ILogger _logger;

		public HigherAspMinorVersionRule(ILogger logger)
		{
			_logger = logger;
		}

		public void Evaluate(VersionRulesContext context)
		{
			if (context.AspVersionModel.Minor > context.CosmosVersionModel.Minor)
			{
				_logger.LogError("CosmosDb have older version of dto than this used in ASP.NET!");
				throw new DtoVersionException(
					$"Version used in CosmosDb have lower Minor number than Dto version used in ASP.NET app. Update CosmosDto! \n Cosmos: {context.CosmosVersionModel.Minor} \n ASP.NET: {context.AspVersionModel.Minor}");
			}
		}

		public bool CanEvaluateRule(VersionRulesContext context) => context.AspVersionModel.Major == context.CosmosVersionModel.Major;
	}
}
