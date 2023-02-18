using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using TddXt.AnyRoot.Strings;
using WotPersonalDataCollector.Api;
using WotPersonalDataCollector.Api.Http;
using WotPersonalDataCollector.Api.Http.RequestObjects;
using static TddXt.AnyRoot.Root;

namespace WotPersonalDataCollector.Tests.Api.Http
{
    [TestFixture]
    public class UserRequestMessageFactoryTests
    {
        private IUserRequestMessageFactory _uut;
        private IApiUriFactory _apiUriFactory;

        [SetUp]
        public void SetUp()
        {
            _apiUriFactory = Substitute.For<IApiUriFactory>();
            _uut = new UserRequestMessageFactory(_apiUriFactory);
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
