namespace WotPersonalDataCollectorWebApp.CosmosDb.Dto.Version.RuleEngine.Factory
{
	using Rules;
	internal class RulesFactory: IRulesFactory
	{
		private readonly ILogger _logger;

		public RulesFactory(ILogger logger)
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
