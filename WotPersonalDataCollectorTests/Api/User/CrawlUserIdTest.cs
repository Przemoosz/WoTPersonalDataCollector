using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using WotPersonalDataCollector.Api.Http;
using static TddXt.AnyRoot.Root;
using TddXt.AnyRoot.Strings;
using WotPersonalDataCollector.Api;
using WotPersonalDataCollector.Api.User;
using WotPersonalDataCollector.Api.Http;

namespace WotPersonalDataCollectorTests.Api.User
{
    [TestFixture]
    public class CrawlUserIdTest
    {
        private IHttpClientWrapperFactory _httpClientWrapperFactory;
        private IHttpRequestMessageFactory _httpRequestMessageFactory;
        private ICrawlUserId _uut;
        private IHttpClientWrapper _httpClientWrapper;

        [SetUp]
        public void SetUp()
        {
            _httpClientWrapper = Substitute.For<IHttpClientWrapper>();
            _httpClientWrapperFactory = Substitute.For<IHttpClientWrapperFactory>();
            _httpRequestMessageFactory = Substitute.For<IHttpRequestMessageFactory>();
            _uut = new CrawlUserId(_httpClientWrapperFactory, _httpRequestMessageFactory);
        }

        [Test]
        public async Task ShouldReturnHttpResponseMessage()
        {
            // Arrange
            var requestMessage = Any.Instance<HttpRequestMessage>();
            var responseMessage = Any.Instance<HttpResponseMessage>();
            _httpClientWrapperFactory.Create().Returns(_httpClientWrapper);
            _httpRequestMessageFactory.Create("https://api.worldoftanks.eu/wot/account/list/").Returns(requestMessage);
            _httpClientWrapper.PostAsync(requestMessage).Returns(responseMessage);

            // Act
            var actual = await _uut.GetUserApiResponseAsync();

            // Assert
            actual.Should().Be(responseMessage);

            // Act

            // Assert

        }
    }
}
