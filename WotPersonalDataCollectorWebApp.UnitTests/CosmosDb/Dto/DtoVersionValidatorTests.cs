using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using WotPersonalDataCollectorWebApp.CosmosDb.Dto.Version;
using WotPersonalDataCollectorWebApp.Utilities;
using static TddXt.AnyRoot.Root;

namespace WotPersonalDataCollectorWebApp.UnitTests.CosmosDb.Dto
{
	[TestFixture]
	public class DtoVersionValidatorTests
	{
		private ILogger _logger;
		private IDtoVersionFactory _dtoVesrionFactory;
		private IAspConfiguration _aspConfiguration;
		private IDtoVersionValidator _uut;

		[SetUp]
		public void SetUp()
		{
			_aspConfiguration = Substitute.For<IAspConfiguration>();
			_logger = Substitute.For<ILogger>();
			_dtoVesrionFactory = Substitute.For<IDtoVersionFactory>();
			_uut = new DtoVersionValidator(_aspConfiguration, _dtoVesrionFactory, _logger);
		}

		[Test]
		public void ShouldNotThrowAnyExceptionAndCreateInformationLogWhenVersionsAreCorrect()
		{

		}

	}
}
