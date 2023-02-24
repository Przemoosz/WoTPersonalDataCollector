namespace WotPersonalDataCollector.WebApp.UnitTests.CosmosDb.Dto.RuleEngine.Rules
{
	using Microsoft.Extensions.Logging;
	using WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version;
	using WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version.RuleEngine;
	using WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version.RuleEngine.Rules;
	using Exceptions;
	using Categories;


	[TestFixture, RuleTests]
	public class LowerAspMajorVersionRuleTests
	{
		private ILogger _logger;
		private IVersionRule _uut;

		[SetUp]
		public void SetUp()
		{
			_logger = Substitute.For<ILogger>();
			_uut = new LowerAspMajorVersionRule(_logger);
		}

		[Test] 
		public void ShouldEvaluateWhenAspMajorAreLowerThanCosmos()
		{
			// Arrange
			VersionRulesContext context = new VersionRulesContext()
			{
				AspVersionModel = new SemanticVersionModel(1, 1, 1),
				CosmosVersionModel = new SemanticVersionModel(2, 1, 1)
			};

			// Act
			Action act = () => _uut.Evaluate(context);
			var shouldEvaluate = _uut.CanEvaluateRule(context);

			// Assert
			shouldEvaluate.Should().BeTrue();
			act.Should().ThrowExactly<DtoVersionException>();
			_logger.ReceivedWithAnyArgs(1).LogError(default);
		}
	}
}
