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
        private IUserInfoRequestMessageFactory _userInfoRequestMessageFactory;
        private UserInfoRequestMessageCreateStep _uut;

        [SetUp]
        public void SetUp()
        {
            _userInfoRequestMessageFactory = Substitute.For<IUserInfoRequestMessageFactory>();
            _uut = new UserInfoRequestMessageCreateStep(_userInfoRequestMessageFactory);
        }

        [Test]
        public async Task ShouldCreateHttpRequestMessage()
        {
            // Arrange
            var requestMessage = Any.Instance<HttpRequestMessage>();
            var context = Any.Instance<WorkflowContext>();
            context.UserInfoRequestMessage = null;
            _userInfoRequestMessageFactory.Create(context.UserInfoApiUrlWithParameters).Returns(requestMessage);

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
            _userInfoRequestMessageFactory.Create(context.UserInfoApiUrlWithParameters).Throws(new Exception());

            // Act
            await _uut.ExecuteInner(context);

            // Assert
            context.UserInfoRequestMessage.Should().BeNull();
            _uut.SuccessfulStatus().Should().BeFalse();
            context.UnexpectedException.Should().BeTrue();
        }
    }
}
