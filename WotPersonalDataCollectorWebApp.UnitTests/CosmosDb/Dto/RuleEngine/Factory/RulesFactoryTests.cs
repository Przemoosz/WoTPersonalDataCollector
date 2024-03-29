﻿using Microsoft.Extensions.Logging;
using WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version.RuleEngine.Factory;
using WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version.RuleEngine.Rules;
using WotPersonalDataCollector.WebApp.UnitTests.Categories;

namespace WotPersonalDataCollector.WebApp.UnitTests.CosmosDb.Dto.RuleEngine.Factory
{
	[TestFixture, FactoryTests, Parallelizable]
	public class RulesFactoryTests
	{
		private ILogger<RulesFactory> _logger;
		private IRulesFactory _uut;

		[SetUp]
		public void SetUp()
		{
			_logger = Substitute.For<ILogger<RulesFactory>>();
			_uut = new RulesFactory(_logger);
		}

		[Test]
		public void ShouldCreateHigherAspMajorVersionRule()
		{
			// Act
			var actual = _uut.CreateHigherAspMajorVersionRule();

			// Assert
			actual.Should().NotBeNull();
			actual.Should().BeAssignableTo<IVersionRule>();
			actual.Should().BeOfType<HigherAspMajorVersionRule>();
		}

		[Test]
		public void ShouldCreateHigherAspMinorVersionRule()
		{
			// Act
			var actual = _uut.CreateHigherAspMinorVersionRule();

			// Assert
			actual.Should().NotBeNull();
			actual.Should().BeAssignableTo<IVersionRule>();
			actual.Should().BeOfType<HigherAspMinorVersionRule>();
		}

		[Test]
		public void ShouldCreateHigherAspPatchVersionRule()
		{
			// Act
			var actual = _uut.CreateHigherAspPatchVersionRule();

			// Assert
			actual.Should().NotBeNull();
			actual.Should().BeAssignableTo<IVersionRule>();
			actual.Should().BeOfType<HigherAspPatchVersionRule>();
		}

		[Test]
		public void ShouldCreateLowerAspMajorVersionRule()
		{
			// Act
			var actual = _uut.CreateLowerAspMajorVersionRule();

			// Assert
			actual.Should().NotBeNull();
			actual.Should().BeAssignableTo<IVersionRule>();
			actual.Should().BeOfType<LowerAspMajorVersionRule>();
		}

		[Test]
		public void ShouldCreateLowerAspMinorVersionRule()
		{
			// Act
			var actual = _uut.CreateLowerAspMinorVersionRule();

			// Assert
			actual.Should().NotBeNull();
			actual.Should().BeAssignableTo<IVersionRule>();
			actual.Should().BeOfType<LowerAspMinorVersionRule>();
		}

		[Test]
		public void ShouldCreateLowerAspPatchVersionRule()
		{
			// Act
			var actual = _uut.CreateLowerAspPatchVersionRule();

			// Assert
			actual.Should().NotBeNull();
			actual.Should().BeAssignableTo<IVersionRule>();
			actual.Should().BeOfType<LowerAspPatchVersionRule>();
		}

		[Test]
		public void ShouldCreateAspVersionEqualsCosmosVersionRule()
		{
			// Act
			var actual = _uut.CreateAspVersionEqualsCosmosVersionRule();

			// Assert
			actual.Should().NotBeNull();
			actual.Should().BeAssignableTo<IVersionRule>();
			actual.Should().BeOfType<AspVersionEqualsCosmosVersionRule>();
		}
	}
}
