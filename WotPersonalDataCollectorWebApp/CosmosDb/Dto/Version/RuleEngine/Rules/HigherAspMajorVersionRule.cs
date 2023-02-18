namespace WotPersonalDataCollectorWebApp.CosmosDb.Dto.Version.RuleEngine.Rules
{
	using Exceptions;
	internal sealed class HigherAspMajorVersionRule: IVersionRule
	{
		private readonly ILogger _logger;

		public HigherAspMajorVersionRule(ILogger logger)
		{
			_logger = logger;
		}

		public void Evaluate(VersionRulesContext context)
		{
			if (context.AspVersionModel.Major > context.CosmosVersionModel.Major)
			{
				_logger.LogError("CosmosDb have lower major version number than this used in ASP.NET!");
				throw new DtoVersionException($"Version used in CosmosDb have lower Major number than Dto version used in ASP.NET app. Update CosmosDto! \n Cosmos: {context.CosmosVersionModel.Major} \n ASP.NET: {context.AspVersionModel.Major}");
			}
		}

		public bool CanEvaluateRule(VersionRulesContext context) => true;
	}
}
