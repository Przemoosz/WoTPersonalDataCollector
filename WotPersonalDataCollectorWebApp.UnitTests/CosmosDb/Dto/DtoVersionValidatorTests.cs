using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using WotPersonalDataCollectorWebApp.CosmosDb.Dto;
using WotPersonalDataCollectorWebApp.CosmosDb.Dto.Version;
using WotPersonalDataCollectorWebApp.Exceptions;
using WotPersonalDataCollectorWebApp.Utilities;
using static TddXt.AnyRoot.Root;

namespace WotPersonalDataCollectorWebApp.UnitTests.CosmosDb.Dto
{
	[TestFixture]
	public class DtoVersionValidatorTests
	{
		private ILogger _logger;
		private ISemanticVersionModelFactory _semanticVesrionModelFactory;
		private IAspConfiguration _aspConfiguration;
		private IDtoVersionValidator _uut;

		[SetUp]
		public void SetUp()
		{
			_aspConfiguration = Substitute.For<IAspConfiguration>();
			_logger = Substitute.For<ILogger>();
			_semanticVesrionModelFactory = Substitute.For<ISemanticVersionModelFactory>();
			_uut = new DtoVersionValidator(_aspConfiguration, _semanticVesrionModelFactory, _logger);
		}

		[Test]
		public void ShouldNotThrowAnyExceptionAndCreateInformationLogWhenVersionsAreEqual()
		{
			// Arrange
			string dtoVersion = "3.12.9";
			UserPersonalData userPersonalData = Any.Instance<UserPersonalData>();
			SemanticVersionModel semanticVersionModelObject = new SemanticVersionModel(3, 12, 9);
			userPersonalData.ClassProperties.DtoVersion = dtoVersion;
			_aspConfiguration.WotDtoVersion.Returns(dtoVersion);
			_semanticVesrionModelFactory.Create(dtoVersion).Returns(semanticVersionModelObject);

			// Act
			Action act = () => _uut.EnsureVersionCorrectness(userPersonalData);

			// Assert
			act.Should().NotThrow();
			 _logger.ReceivedWithAnyArgs(1).LogInformation(default);
		}

		[Test]
		public void ShouldThrowDtoVersionExceptionWhenCosmosDbVersionIsHigherThanInAspApp()
		{
			// Arrange
			string cosmosDtoVersion = "3.12.9";
			string aspDtoVersion = "2.14.19";
			UserPersonalData userPersonalData = Any.Instance<UserPersonalData>();
			SemanticVersionModel cosmosSemanticVersionModelObject = new SemanticVersionModel(3, 12, 9);
			SemanticVersionModel aspSemanticVersionModelObject = new SemanticVersionModel(2, 14, 19);
			userPersonalData.ClassProperties.DtoVersion = cosmosDtoVersion;
			_aspConfiguration.WotDtoVersion.Returns(aspDtoVersion);
			_semanticVesrionModelFactory.Create(aspDtoVersion).Returns(aspSemanticVersionModelObject);
			_semanticVesrionModelFactory.Create(cosmosDtoVersion).Returns(cosmosSemanticVersionModelObject);

			// Act
			Action act = () => _uut.EnsureVersionCorrectness(userPersonalData);

			// Assert
			act.Should().Throw<DtoVersionException>();
			_logger.Received(1).LogError("CosmosDb have higher version of dto than this used in ASP.NET!");
		}

		[Test]
		public void ShouldThrowDtoVersionExceptionWhenCosmosDbVersionIsLowerThanInAspApp()
		{
			// Arrange
			string cosmosDtoVersion = "2.12.9";
			string aspDtoVersion = "3.14.19";
			UserPersonalData userPersonalData = Any.Instance<UserPersonalData>();
			SemanticVersionModel cosmosSemanticVersionModelObject = new SemanticVersionModel(2, 12, 9);
			SemanticVersionModel aspSemanticVersionModelObject = new SemanticVersionModel(3, 14, 19);
			userPersonalData.ClassProperties.DtoVersion = cosmosDtoVersion;
			_aspConfiguration.WotDtoVersion.Returns(aspDtoVersion);
			_semanticVesrionModelFactory.Create(aspDtoVersion).Returns(aspSemanticVersionModelObject);
			_semanticVesrionModelFactory.Create(cosmosDtoVersion).Returns(cosmosSemanticVersionModelObject);

			// Act
			Action act = () => _uut.EnsureVersionCorrectness(userPersonalData);

			// Assert
			act.Should().Throw<DtoVersionException>();
			_logger.Received(1).LogError("CosmosDb have lower major version number than this used in ASP.NET!");
		}

	}
}
