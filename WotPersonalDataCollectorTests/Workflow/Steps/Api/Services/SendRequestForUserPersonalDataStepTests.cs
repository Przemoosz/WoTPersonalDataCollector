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

namespace WotPersonalDataCollector.Tests.Workflow.Steps.Api.Services
{
    [TestFixture]
    public class SendRequestForUserPersonalDataStepTests
    {
        private IWotService _wotService;
        private SendRequestForUserPersonalDataStep _uut;

        [SetUp]
        public void SetUp()
        {
            _wotService = Substitute.For<IWotService>();
            _uut = new SendRequestForUserPersonalDataStep(_wotService);
        }

        [Test]
        public async Task ShouldReturnHttpResponseMessage()
        {
            // Arrange
            var context = Any.Instance<WorkflowContext>();
            context.UserPersonalDataResponseMessage = null;
            var responseMessage = Any.Instance<HttpResponseMessage>();
            _wotService.GetUserApiResponseAsync(context.UserPersonalDataRequestMessage).Returns(responseMessage);

            // Act 
            await _uut.ExecuteInner(context);

            // Assert
            context.UserPersonalDataResponseMessage.Should().NotBeNull();
            context.UserPersonalDataResponseMessage.Should().Be(responseMessage);
            _uut.SuccessfulStatus().Should().BeTrue();
        }

        [Test]
        public async Task ShouldSetSuccessfulStatusToFalseWhenHttpExceptionIsThrown()
        {
            // Assert
            var context = Any.Instance<WorkflowContext>();
            context.UserPersonalDataResponseMessage = null;
            context.UnexpectedException = false;
            _wotService.GetUserApiResponseAsync(context.UserPersonalDataRequestMessage).ThrowsAsync(new HttpRequestException());

            // Act
            await _uut.ExecuteInner(context);

            // Assert
            _uut.SuccessfulStatus().Should().BeFalse();
            context.UserPersonalDataResponseMessage.Should().BeNull();
            context.UnexpectedException.Should().BeFalse();
        }

        [Test]
        public async Task ShouldSetSuccessfulStatusToFalseWhenAnyExceptionIsThrown()
        {
            // Assert
            var context = Any.Instance<WorkflowContext>();
            context.UserPersonalDataResponseMessage = null;
            context.UnexpectedException = false;
            _wotService.GetUserApiResponseAsync(context.UserPersonalDataRequestMessage).ThrowsAsync(new Exception());

            // Act
            await _uut.ExecuteInner(context);

            // Assert
            _uut.SuccessfulStatus().Should().BeFalse();
            context.UserPersonalDataResponseMessage.Should().BeNull();
            context.UnexpectedException.Should().BeTrue();
        }
    }
}
