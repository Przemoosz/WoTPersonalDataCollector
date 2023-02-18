using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using WotPersonalDataCollector.CosmosDb;
using WotPersonalDataCollector.Utilities;
using static TddXt.AnyRoot.Root;

namespace WotPersonalDataCollector.Tests.CosmosDb
{
    [TestFixture]
    public class WpdCosmosClientWrapperFactoryTests
    {
        private IConfiguration _configuration;
        private IWpdCosmosClientWrapperFactory _uut;

        [SetUp]
        public void SetUp()
        {
            _configuration = Substitute.For<IConfiguration>();
            _configuration.CosmosConnectionString.Returns(Any.CosmosDbConnectionString());
            _uut = new WpdCosmosClientWrapperFactory(_configuration);
        }

        [Test]
        public void ShouldReturnWpdCosmosClientWrapper()
        {
            // Act
            var actual = _uut.Create();

            // Assert
            actual.Should().BeOfType<WpdCosmosClientWrapper>();
            actual.Should().BeAssignableTo<IWpdCosmosClientWrapper>();
        }

        [Test]
        public void ShouldReturnSameWpdCosmosClientWrapper()
        {
            // Act
            var actual = _uut.Create();
            var actual1 = _uut.Create();
            var actual2 = _uut.Create();

            // Assert
            actual.Should().BeSameAs(actual1);
            actual.Should().BeSameAs(actual2);
            actual2.Should().BeSameAs(actual1);
        }
    }
}
