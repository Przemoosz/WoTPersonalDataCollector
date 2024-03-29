﻿namespace WotPersonalDataCollector.Tests.Workflow.Steps.Api.Services
{
	using System;
	using System.Net.Http;
	using System.Threading.Tasks;
	using NSubstitute.ExceptionExtensions;
	using WotPersonalDataCollector.Api.Services;
	using WotPersonalDataCollector.Workflow;
	using WotPersonalDataCollector.Workflow.Steps.Api.Services;
	using static TddXt.AnyRoot.Root;

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
            _wotService.GetUserApiResponseAsync(context.UserInfoRequestMessage).Returns(responseMessage);

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
            _wotService.GetUserApiResponseAsync(context.UserInfoRequestMessage).ThrowsAsync(new HttpRequestException());

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
            _wotService.GetUserApiResponseAsync(context.UserInfoRequestMessage).ThrowsAsync(new Exception());

            // Act
            await _uut.ExecuteInner(context);

            // Assert
            _uut.SuccessfulStatus().Should().BeFalse();
            context.UserIdResponseMessage.Should().BeNull();
            context.UnexpectedException.Should().BeTrue();
        }
    }
}
