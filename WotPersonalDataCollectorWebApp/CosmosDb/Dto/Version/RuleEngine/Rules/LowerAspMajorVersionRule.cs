using WotPersonalDataCollectorWebApp.Exceptions;

namespace WotPersonalDataCollectorWebApp.CosmosDb.Dto.Version.RuleEngine.Rules
{
	internal sealed class LowerAspMajorVersionRule : IVersionRule
	{
		private readonly ILogger _logger;

		public LowerAspMajorVersionRule(ILogger logger)
		{
			_logger = logger;
		}

		public void Evaluate(VersionRulesContext context)
		{
			if (context.AspVersionModel.Major > context.CosmosVersionModel.Major)
			{
				_logger.LogError("CosmosDb have higher version of dto than this used in ASP.NET!");
				throw new DtoVersionException($"Version used in CosmosDb have higher Major number than Dto version used in ASP.NET app. Update ASP.NET! \n Cosmos: {context.CosmosVersionModel.Major} \n ASP.NET: {context.AspVersionModel.Major}");
			}
		}

		public bool CanEvaluateRule(VersionRulesContext context) => true;
	}
}
