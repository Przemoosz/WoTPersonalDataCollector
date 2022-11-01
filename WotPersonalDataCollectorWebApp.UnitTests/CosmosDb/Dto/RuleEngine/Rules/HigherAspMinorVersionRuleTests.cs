using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Microsoft.Extensions.Logging;
using WotPersonalDataCollectorWebApp.CosmosDb.Dto.Version.RuleEngine.Rules;
using WotPersonalDataCollectorWebApp.CosmosDb.Dto.Version.RuleEngine;
using WotPersonalDataCollectorWebApp.CosmosDb.Dto.Version;
using WotPersonalDataCollectorWebApp.Exceptions;
using WotPersonalDataCollectorWebApp.UnitTests.Categories;

namespace WotPersonalDataCollectorWebApp.UnitTests.CosmosDb.Dto.RuleEngine.Rules
{
	[TestFixture, RuleTests]
	public class HigherAspMinorVersionRuleTests
	{
		private ILogger _logger;
		private IVersionRule _uut;

		[SetUp]
		public void SetUp()
		{
			_logger = Substitute.For<ILogger>();
			_uut = new HigherAspMinorVersionRule(_logger);
		}

		[Test]
		public void ShouldEvaluateWhenAspMinorVersionAreHigherThanCosmos()
		{
			// Arrange
			VersionRulesContext context = new VersionRulesContext()
			{
				AspVersionModel = new SemanticVersionModel(1, 21, 1),
				CosmosVersionModel = new SemanticVersionModel(1, 1, 1)
			};

			// Act
			Action act = () => _uut.Evaluate(context);
			var shouldEvaluate = _uut.CanEvaluateRule(context);

			// Assert
			shouldEvaluate.Should().BeTrue();
			act.Should().ThrowExactly<DtoVersionException>();
			_logger.ReceivedWithAnyArgs(1).LogError(default);
		}

		[Test]
		public void ShouldNotExecuteWhenMajorVersionNotEqual()
		{
			// Arrange
			VersionRulesContext context = new VersionRulesContext()
			{
				AspVersionModel = new SemanticVersionModel(2, 21, 1),
				CosmosVersionModel = new SemanticVersionModel(1, 1, 1)
			};

			// Act
			var actual = _uut.CanEvaluateRule(context);

			// Assert
			actual.Should().BeFalse();
		}
	}
}
