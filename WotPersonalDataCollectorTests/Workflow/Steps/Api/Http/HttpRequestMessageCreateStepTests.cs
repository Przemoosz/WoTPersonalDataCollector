﻿namespace WotPersonalDataCollector.Tests.Workflow.Steps.Api.Http
{
	using System;
	using System.Net.Http;
	using System.Threading.Tasks;
	using NSubstitute.ExceptionExtensions;
	using WotPersonalDataCollector.Api.Http;
	using WotPersonalDataCollector.Workflow;
	using WotPersonalDataCollector.Workflow.Steps.Api.Http;
	using static TddXt.AnyRoot.Root;

	[TestFixture]
    public class HttpRequestMessageCreateStepTests
    {
        private IUserRequestMessageFactory _userRequestMessageFactory;
        private UserInfoRequestMessageStep _uut;

        [SetUp]
        public void SetUp()
        {
            _userRequestMessageFactory = Substitute.For<IUserRequestMessageFactory>();
            _uut = new UserInfoRequestMessageStep(_userRequestMessageFactory);
        }

        [Test]
        public async Task ShouldCreateHttpRequestMessage()
        {
            // Arrange
            var requestMessage = Any.Instance<HttpRequestMessage>();
            var context = Any.Instance<WorkflowContext>();
            context.UserInfoRequestMessage = null;
            _userRequestMessageFactory.Create(context.UserInfoApiUriWithParameters).Returns(requestMessage);

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
            _userRequestMessageFactory.Create(context.UserInfoApiUriWithParameters).Throws(new Exception());

            // Act
            await _uut.ExecuteInner(context);

            // Assert
            context.UserInfoRequestMessage.Should().BeNull();
            _uut.SuccessfulStatus().Should().BeFalse();
            context.UnexpectedException.Should().BeTrue();
        }
    }
}
