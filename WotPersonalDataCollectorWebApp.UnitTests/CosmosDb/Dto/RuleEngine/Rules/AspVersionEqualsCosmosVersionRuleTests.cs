namespace WotPersonalDataCollector.WebApp.UnitTests.CosmosDb.Dto.RuleEngine.Rules
{
	using Microsoft.Extensions.Logging;
	using WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version;
	using WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version.RuleEngine;
	using WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version.RuleEngine.Rules;
	using Categories;

	[TestFixture, RuleTests]
	public class AspVersionEqualsCosmosVersionRuleTests
	{
		private ILogger _logger;
		private IVersionRule _uut;

		[SetUp]
		public void SetUp()
		{
			_logger = Substitute.For<ILogger>();
			_uut = new AspVersionEqualsCosmosVersionRule(_logger);
		}

		[Test]
		public void ShouldEvaluateWhenVersionAreEqual()
		{
			// Arrange
			VersionRulesContext context = new VersionRulesContext()
			{
				AspVersionModel = new SemanticVersionModel(1, 1, 1),
				CosmosVersionModel = new SemanticVersionModel(1, 1, 1)
			};

			// Act
			_uut.Evaluate(context);
			var shouldEvaluate = _uut.CanEvaluateRule(context);
			// Assert
			shouldEvaluate.Should().BeTrue();
			_logger.ReceivedWithAnyArgs(1).LogInformation(default);
		}

		[Test]
		public void ShouldNotEvaluateWhenVersionNotEquals()
		{
			// Arrange
			VersionRulesContext context = new VersionRulesContext()
			{
				AspVersionModel = new SemanticVersionModel(2, 3, 4),
				CosmosVersionModel = new SemanticVersionModel(1, 1, 1)
			};

			// Act
			var shouldEvaluate = _uut.CanEvaluateRule(context);

			// Assert
			shouldEvaluate.Should().BeFalse();
		}
	}
}
