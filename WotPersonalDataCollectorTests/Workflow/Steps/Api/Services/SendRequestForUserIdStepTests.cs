using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using WotPersonalDataCollector.Api.Services;
using WotPersonalDataCollector.Workflow;
using WotPersonalDataCollector.Workflow.Steps.Api.Services;
using static TddXt.AnyRoot.Root;

namespace WotPersonalDataCollectorTests.Workflow.Steps.Api.Services
{
    [TestFixture]
    public class SendRequestForUserIdStepTests
    {
        private IWotService _wotService;
        private SendRequestForUserIdStep _uut;

        [SetUp]
        public void SetUp()
        {
            _wotService = Substitute.For<IWotService>();
            _uut = new SendRequestForUserIdStep(_wotService);
        }

        [Test]
        public async Task ShouldReturnHttpResponseMessage()
        {
            // Arrange
            var context = Any.Instance<WorkflowContext>();
            context.UserIdResponseMessage = null;
            var responseMessage = Any.Instance<HttpResponseMessage>();
            _wotService.GetUserIdApiResponseAsync(context.UserInfoRequestMessage).Returns(responseMessage);

            // Act 
            await _uut.ExecuteInner(context);

            // Assert
            context.UserIdResponseMessage.Should().NotBeNull();
            context.UserIdResponseMessage.Should().Be(responseMessage);
            _uut.SuccessfulStatus().Should().BeTrue();
        }

        [Test]
        public async Task ShouldSetSuccessfulStatusToFalseWhenHttpExceptionIsThrown()
        {
            // Assert
            var context = Any.Instance<WorkflowContext>();
            context.UserIdResponseMessage = null;
            context.UnexpectedException = false;
            _wotService.GetUserIdApiResponseAsync(context.UserInfoRequestMessage).ThrowsAsync(new Exception());

            // Act
            await _uut.ExecuteInner(context);

            // Assert
            _uut.SuccessfulStatus().Should().BeFalse();
            context.UserIdResponseMessage.Should().BeNull();
            context.UnexpectedException.Should().BeFalse();
        }

        [Test]
        public async Task ShouldSetSuccessfulStatusToFalseWhenAnyExceptionIsThrown()
        {
            // Assert
            var context = Any.Instance<WorkflowContext>();
            context.UserIdResponseMessage = null;
            context.UnexpectedException = false;
            _wotService.GetUserIdApiResponseAsync(context.UserInfoRequestMessage).ThrowsAsync(new Exception());

            // Act
            await _uut.ExecuteInner(context);

            // Assert
            _uut.SuccessfulStatus().Should().BeFalse();
            context.UserIdResponseMessage.Should().BeNull();
            context.UnexpectedException.Should().BeTrue();
        }
    }
}
