using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using WotPersonalDataCollector.Api.Http;
using static TddXt.AnyRoot.Root;
using TddXt.AnyRoot.Strings;
using WotPersonalDataCollector.Api;

namespace WotPersonalDataCollectorTests.Api.Http
{
    [TestFixture]
    public class HttpRequestMessageFactoryTests
    {
        private IHttpRequestMessageFactory _uut;
        private IApiUrlFactory _apiUrlFactory;

        [SetUp]
        public void SetUp()
        {
            _apiUrlFactory = Substitute.For<IApiUrlFactory>();
            _uut = new HttpRequestMessageFactory(_apiUrlFactory);
        }

        [Test]
        public void ShouldReturnHttpRequestMessage()
        {
            // Arrange
            var apiUrl = Any.String();
            _apiUrlFactory.Create(apiUrl).Returns(apiUrl);
            // Actual
            var actual = _uut.Create(apiUrl);

            // Assert
            actual.RequestUri.Should().Be(apiUrl);
        }
    }
}
