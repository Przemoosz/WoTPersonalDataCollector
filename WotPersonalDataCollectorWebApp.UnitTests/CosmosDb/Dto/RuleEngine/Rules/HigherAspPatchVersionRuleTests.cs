using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version;
using WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version.RuleEngine;
using WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version.RuleEngine.Rules;
using WotPersonalDataCollector.WebApp.UnitTests.Categories;

namespace WotPersonalDataCollector.WebApp.UnitTests.CosmosDb.Dto.RuleEngine.Rules
{
	[TestFixture, RuleTests]
	public class HigherAspPatchVersionRuleTests
	{
		private ILogger _logger;
		private IVersionRule _uut;

		[SetUp]
		public void SetUp()
		{
			_logger = Substitute.For<ILogger>();
			_uut = new HigherAspPatchVersionRule(_logger);
		}

		[Test]
		public void ShouldEvaluateWhenAspMinorVersionAreHigherThanCosmos()
		{
			// Arrange
			VersionRulesContext context = new VersionRulesContext()
			{
				AspVersionModel = new SemanticVersionModel(1, 1, 23),
				CosmosVersionModel = new SemanticVersionModel(1, 1, 1)
			};

			// Act
			_uut.Evaluate(context);
			var shouldEvaluate = _uut.CanEvaluateRule(context);

			// Assert
			shouldEvaluate.Should().BeTrue();
			_logger.ReceivedWithAnyArgs(1).LogWarning(default);
		}

		[TestCase(1,3,1,1)]
		[TestCase(1, 1, 24, 1)]
		public void ShouldNotExecuteWhenMajorOrMinorVersionNotEqual(int aspMajor, int cosmosMajor, int aspMinor, int cosmosMinor)
		{
			// Arrange
			VersionRulesContext context = new VersionRulesContext()
			{
				AspVersionModel = new SemanticVersionModel(aspMajor, aspMinor, 1),
				CosmosVersionModel = new SemanticVersionModel(cosmosMajor,cosmosMinor, 1)
			};

			// Act
			var actual = _uut.CanEvaluateRule(context);

			// Assert
			actual.Should().BeFalse();
		}
	}
}
