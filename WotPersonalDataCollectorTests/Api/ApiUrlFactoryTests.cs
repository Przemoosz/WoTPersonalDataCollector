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
        private ApiUrlFactory _uut;

        [SetUp]
        public void SetUp()
        {
            _userInfoRequestObjectFactory = Substitute.For<IUserInfoRequestObjectFactory>();
            _uut = new ApiUrlFactory(_userInfoRequestObjectFactory);
        }

        [Test]
        public void ShouldReturnApiUrlWithOneQuery()
        {
            // Arrange
            string url = Any.String();
            var requestObject = Any.Instance<IRequestObject>();
            _userInfoRequestObjectFactory.Create().Returns(requestObject);

            // Act
            var actual = _uut.Create(url);

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
            var actual = _uut.Create(url);

            // Assert
            actual.Should().Be(url + "?" + "application_id=" + requestObject.application_id + "&search=" +
                               requestObject.search + "&");
        }
    }
}
