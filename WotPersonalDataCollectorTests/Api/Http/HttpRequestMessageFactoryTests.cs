using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using WotPersonalDataCollector.Api.Http.RequestObjects;
using WotPersonalDataCollector.Api.Http;
using WotPersonalDataCollector.Utilities;
using static TddXt.AnyRoot.Root;
using TddXt.AnyRoot.Strings;

namespace WotPersonalDataCollectorTests.Api.Http
{
    [TestFixture]
    public class HttpRequestMessageFactoryTests
    {
        private IHttpRequestMessageFactory _uut;
        private IRequestObjectFactory _requestObjectFactory;

        [SetUp]
        public void SetUp()
        {
            _requestObjectFactory = Substitute.For<IRequestObjectFactory>();
            _uut = new HttpRequestMessageFactory(_requestObjectFactory);
        }

        [Test]
        public void ShouldReturnHttpRequestMessage()
        {
            // Arrange
            var requestObject = Any.Instance<UserInfoRequestObject>();
            _requestObjectFactory.Create().Returns(requestObject);
            var serializedRequestObject = JsonConvert.SerializeObject(requestObject);
            var apiUrl = Any.String();

            // Actual
            var actual = _uut.Create(apiUrl);

            // Assert
            actual.RequestUri.Should().Be(apiUrl);
            actual.Content.ReadAsStringAsync().GetAwaiter().GetResult().Should().Be(serializedRequestObject);
        }
    }
}
