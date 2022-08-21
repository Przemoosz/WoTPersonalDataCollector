using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WotPersonalDataCollector.Api.Http;
using WotPersonalDataCollector.Workflow;
using WotPersonalDataCollector.Workflow.Steps.Api.Http;
using static TddXt.AnyRoot.Root;

namespace WotPersonalDataCollectorTests.Workflow.Steps.Api.Http
{
    [TestFixture]
    public class HttpRequestMessageCreateStepTests
    {
        private IHttpRequestMessageFactory _httpRequestMessageFactory;
        private HttpRequestMessageCreateStep _uut;

        [SetUp]
        public void SetUp()
        {
            _httpRequestMessageFactory = Substitute.For<IHttpRequestMessageFactory>();
            _uut = new HttpRequestMessageCreateStep(_httpRequestMessageFactory);
        }

        [Test]
        public async Task ShouldCreateHttpRequestMessage()
        {
            // Arrange
            var requestMessage = Any.Instance<HttpRequestMessage>();
            var context = Any.Instance<WorkflowContext>();
            context.UserInfoRequestMessage = null;
            _httpRequestMessageFactory.Create(context.UserInfoApiUrl).Returns(requestMessage);

            // Act
            await _uut.ExecuteInner(context);

            // Assert
            context.UserInfoRequestMessage.Should().NotBeNull();
            context.UserInfoRequestMessage.Should().Be(requestMessage);
            _uut.SuccessfulStatus().Should().BeTrue();
        }

        [Test]
        public async Task ShouldSetSuccessfulStatusToFalseWhenAnyExceptionIsThrown()
        {
            // Arrange 
            var context = Any.Instance<WorkflowContext>();
            context.UserInfoRequestMessage = null;
            context.UnexpectedException = false;
            _httpRequestMessageFactory.Create(context.UserInfoApiUrl).Throws(new Exception());

            // Act
            await _uut.ExecuteInner(context);

            // Assert
            context.UserInfoRequestMessage.Should().BeNull();
            _uut.SuccessfulStatus().Should().BeFalse();
            context.UnexpectedException.Should().BeTrue();
        }
    }
}
