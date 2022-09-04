using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using WotPersonalDataCollector.Api.Http;
using static TddXt.AnyRoot.Root;
using TddXt.AnyRoot.Strings;
using WotPersonalDataCollector.Api;
using WotPersonalDataCollector.Api.Http.RequestObjects;

namespace WotPersonalDataCollectorTests.Api.Http
{
    [TestFixture]
    public class UserInfoRequestMessageFactoryTests
    {
        private IUserInfoRequestMessageFactory _uut;
        private IApiUriFactory _apiUriFactory;

        [SetUp]
        public void SetUp()
        {
            _apiUriFactory = Substitute.For<IApiUriFactory>();
            _uut = new UserInfoRequestMessageFactory(_apiUriFactory);
        }

        [Test]
        public void ShouldReturnHttpRequestMessage()
        {
            // Arrange
            var apiUrl = Any.String();
            var requestObject = Any.Instance<IRequestObject>();
            _apiUriFactory.Create(apiUrl, requestObject).Returns(apiUrl);

            // Actual
            var actual = _uut.Create(apiUrl);

            // Assert
            actual.RequestUri.Should().Be(apiUrl);
        }
    }
}
