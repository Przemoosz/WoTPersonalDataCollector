using System;
using FluentAssertions;
using Newtonsoft.Json;
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
        private IRequestObjectFactory _requestObjectFactory;
        private ApiUrlFactory _uut;

        [SetUp]
        public void SetUp()
        {
            _requestObjectFactory = Substitute.For<IRequestObjectFactory>();
            _uut = new ApiUrlFactory(_requestObjectFactory);
        }

        [Test]
        public void ShouldReturnApiUrlWithOneQuery()
        {
            // Arrange
            string url = Any.String();
            var requestObject = Any.Instance<RequestObject>();
            _requestObjectFactory.Create().Returns(requestObject);

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
            _requestObjectFactory.Create().Returns(requestObject);
            
            // Act
            var actual = _uut.Create(url);

            // Assert
            actual.Should().Be(url + "?" + "application_id=" + requestObject.application_id + "&search=" +
                               requestObject.search + "&");
        }
    }
}
