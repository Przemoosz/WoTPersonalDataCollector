using NSubstitute;
using NUnit.Framework;
using TddXt.AnyRoot.Strings;
using WotPersonalDataCollector.CosmosDb.DTO;
using WotPersonalDataCollector.Utilities;
using static TddXt.AnyRoot.Root;

namespace WotPersonalDataCollectorTests.CosmosDb.DTO
{
    [TestFixture]
    public class WotDataCosmosDbDtoFactoryTests
    {
        private IConfiguration _configuration;
        private IWotDataCosmosDbDtoFactory _uut;

        [SetUp]
        public void SetUp()
        {
            _configuration = Substitute.For<IConfiguration>();
            _uut = new WotDataCosmosDbDtoFactory(_configuration);
        }

        [Test]
        public void ShouldReturnWotDataCosmosDbDto()
        {
            // Arrange
            _configuration.DtoVersion.Returns(Any.String());
            // Act

            // Assert

        }
    }
}
