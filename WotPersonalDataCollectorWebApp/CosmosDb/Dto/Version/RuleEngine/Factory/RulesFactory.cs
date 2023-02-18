using WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version.RuleEngine.Rules;

namespace WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version.RuleEngine.Factory
{
	internal class RulesFactory: IRulesFactory
	{
		private readonly ILogger _logger;

		public RulesFactory(ILogger<RulesFactory> logger)
		{
			_logger = logger;
		}

		public IVersionRule CreateHigherAspMajorVersionRule()
		{
			return new HigherAspMajorVersionRule(_logger);
		}

		public IVersionRule CreateHigherAspMinorVersionRule()
		{
			return new HigherAspMinorVersionRule(_logger);
		}

		public IVersionRule CreateHigherAspPatchVersionRule()
		{
			return new HigherAspPatchVersionRule(_logger);
		}

		public IVersionRule CreateLowerAspMajorVersionRule()
		{
			return new LowerAspMajorVersionRule(_logger);
		}

		public IVersionRule CreateLowerAspMinorVersionRule()
		{
			return new LowerAspMinorVersionRule(_logger);
		}

		public IVersionRule CreateLowerAspPatchVersionRule()
		{
			return new LowerAspPatchVersionRule(_logger);
		}

		public IVersionRule CreateAspVersionEqualsCosmosVersionRule()
		{
			return new AspVersionEqualsCosmosVersionRule(_logger);
		}
	}
}
