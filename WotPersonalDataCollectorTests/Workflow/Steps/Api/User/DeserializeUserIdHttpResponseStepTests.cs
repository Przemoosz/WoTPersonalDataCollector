using System;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WotPersonalDataCollector.Exceptions;
using NUnit.Framework;
using WotPersonalDataCollector.Api.User;
using WotPersonalDataCollector.Api.User.DTO;
using WotPersonalDataCollector.Workflow;
using WotPersonalDataCollector.Workflow.Steps.Api.User;
using static TddXt.AnyRoot.Root;
namespace WotPersonalDataCollectorTests.Workflow.Steps.Api.User
{
    [TestFixture]
    public class DeserializeUserIdHttpResponseStepTests
    {
        private DeserializeUserIdHttpResponseStep _uut;
        private IDeserializeUserIdHttpResponse _deserializeUserIdHttpResponse;

        [SetUp]
        public void SetUp()
        {
            _deserializeUserIdHttpResponse = Substitute.For<IDeserializeUserIdHttpResponse>();
            _uut = new DeserializeUserIdHttpResponseStep(_deserializeUserIdHttpResponse);
        }

        [Test]
        public async Task ShouldReturnUserIdData()
        {
            // Arrange
            var context = Any.Instance<WorkflowContext>();
            context.UserIdData = null;
            var userIdData = Any.Instance<UserIdData>();
            _deserializeUserIdHttpResponse.Deserialize(context.UserIdResponseMessage).Returns(userIdData);

            // Act
            await _uut.ExecuteInner(context);

            // Assert
            context.UserIdData.Should().NotBeNull();
            context.UserIdData.Should().Be(userIdData);
        }

        [Test]
        public async Task ShouldSetSuccessfulStatusToFalseWhenDeserializeExceptionIsThrown()
        {
            // Assert
            var context = Any.Instance<WorkflowContext>();
            context.UnexpectedException = false;
            context.UserIdData = null;
            _deserializeUserIdHttpResponse.Deserialize(context.UserIdResponseMessage).ThrowsAsync(new DeserializeJsonException());

            // Act
            await _uut.ExecuteInner(context);

            // Assert
            _uut.SuccessfulStatus().Should().BeFalse();
            context.UserIdData.Should().BeNull();
            context.UnexpectedException.Should().BeFalse();
        }

        [Test]
        public async Task ShouldSetSuccessfulStatusToFalseWhenWotApiResponseExceptionIsThrown()
        {
            // Arrange
            var context = Any.Instance<WorkflowContext>();
            context.UnexpectedException = false;
            context.UserIdData = null;
            _deserializeUserIdHttpResponse.Deserialize(context.UserIdResponseMessage).ThrowsAsync(new WotApiResponseException());

            // Act
            await _uut.ExecuteInner(context);

            // Assert
            _uut.SuccessfulStatus().Should().BeFalse();
            context.UserIdData.Should().BeNull();
            context.UnexpectedException.Should().BeFalse();
        }

        [Test]
        public async Task ShouldSetSuccessfulStatusToFalseWhenMoreThanOneUserExceptionIsThrown()
        {
            // Arrange
            var context = Any.Instance<WorkflowContext>();
            context.UnexpectedException = false;
            context.UserIdData = null;
            _deserializeUserIdHttpResponse.Deserialize(context.UserIdResponseMessage).ThrowsAsync(new MoreThanOneUserException());

            // Act
            await _uut.ExecuteInner(context);

            // Assert
            _uut.SuccessfulStatus().Should().BeFalse();
            context.UserIdData.Should().BeNull();
            context.UnexpectedException.Should().BeFalse();
        }

        [Test]
        public async Task ShouldSetSuccessfulStatusToFalseWhenAnyExceptionIsThrown()
        {
            // Arrange
            var context = Any.Instance<WorkflowContext>();
            context.UnexpectedException = false;
            context.UserIdData = null;
            _deserializeUserIdHttpResponse.Deserialize(context.UserIdResponseMessage).ThrowsAsync(new Exception());

            // Act
            await _uut.ExecuteInner(context);

            // Assert
            _uut.SuccessfulStatus().Should().BeFalse();
            context.UserIdData.Should().BeNull();
            context.UnexpectedException.Should().BeTrue();
        }
    }
}
