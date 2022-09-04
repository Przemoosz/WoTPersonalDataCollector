using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using WotPersonalDataCollector.Api.Http.RequestObjects;
using WotPersonalDataCollector.Api;
using static TddXt.AnyRoot.Root;
using TddXt.AnyRoot.Strings;

namespace WotPersonalDataCollectorTests.Api
{
    [TestFixture]
    public class ApiUrlFactoryTests
    {
        private IUserInfoRequestObjectFactory _userInfoRequestObjectFactory;
        private ApiUriFactory _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new ApiUriFactory();
        }

        [Test]
        public void ShouldReturnApiUrlWithOneQuery()
        {
            // Arrange
            string url = Any.String();
            var requestObject = Any.Instance<IRequestObject>();

            // Act
            var actual = _uut.Create(url, requestObject);

            // Assert
            actual.Should().Be(url + "?" + "application_id=" + requestObject.application_id + "&");
        }

        [Test]
        public void ShouldReturnApiUrlWithTwoQuery()
        {
            // Arrange
            string url = Any.String();
            var requestObject = Any.Instance<UserInfoRequestObject>();
            _userInfoRequestObjectFactory.Create().Returns(requestObject);
            
            // Act
            var actual = _uut.Create(url, requestObject);

            // Assert
            actual.Should().Be(url + "?" + "application_id=" + requestObject.application_id + "&search=" +
                               requestObject.search + "&");
        }
    }
}
