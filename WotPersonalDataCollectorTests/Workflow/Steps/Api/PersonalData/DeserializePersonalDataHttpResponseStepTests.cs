using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;
using NSubstitute.ExceptionExtensions;
using WotPersonalDataCollector.Api.PersonalData;
using WotPersonalDataCollector.Api.PersonalData.Dto;
using WotPersonalDataCollector.Exceptions;
using WotPersonalDataCollector.Workflow;
using WotPersonalDataCollector.Workflow.Steps.Api.PersonalData;
using static TddXt.AnyRoot.Root;
using System;

namespace WotPersonalDataCollectorTests.Workflow.Steps.Api.PersonalData
{
    [TestFixture]
    public class DeserializePersonalDataHttpResponseStepTests
    {
        private IDeserializePersonalDataHttpResponse _deserializePersonalDataHttpResponse;
        private DeserializePersonalDataHttpResponseStep _uut;

        [SetUp]
        public void SetUp()
        {
            _deserializePersonalDataHttpResponse = Substitute.For<IDeserializePersonalDataHttpResponse>();
            _uut = new DeserializePersonalDataHttpResponseStep(_deserializePersonalDataHttpResponse);
        }

        [Test]
        public async Task ShouldReturnWotAccountDto()
        {
            // Arrange
            var context = Any.Instance<WorkflowContext>();
            context.AccountDto = null;
            var accountDto = Any.Instance<WotAccountDto>();
            _deserializePersonalDataHttpResponse.Deserialize(context.UserIdResponseMessage, context.ContractResolver).Returns(accountDto);

            // Act
            await _uut.ExecuteInner(context);

            // Assert
            context.AccountDto.Should().NotBeNull();
            _uut.SuccessfulStatus().Should().BeTrue();
            context.AccountDto.Should().Be(accountDto);
        }

        [Test]
        public async Task ShouldSetSuccessfulStatusToFalseWhenDeserializeExceptionIsThrown()
        {
            // Assert
            var context = Any.Instance<WorkflowContext>();
            context.UnexpectedException = false;
            context.AccountDto = null;
            _deserializePersonalDataHttpResponse.Deserialize(context.UserIdResponseMessage, context.ContractResolver).ThrowsAsync(new DeserializeJsonException());

            // Act
            await _uut.ExecuteInner(context);

            // Assert
            _uut.SuccessfulStatus().Should().BeFalse();
            context.AccountDto.Should().BeNull();
            context.UnexpectedException.Should().BeFalse();
        }

        [Test]
        public async Task ShouldSetSuccessfulStatusToFalseWhenUnHandledExceptionIsThrown()
        {
            // Arrange
            var context = Any.Instance<WorkflowContext>();
            context.UnexpectedException = false;
            context.AccountDto = null;
            _deserializePersonalDataHttpResponse.Deserialize(context.UserIdResponseMessage, context.ContractResolver).ThrowsAsync(new Exception());

            // Act
            await _uut.ExecuteInner(context);

            // Assert
            _uut.SuccessfulStatus().Should().BeFalse();
            context.AccountDto.Should().BeNull();
            context.UnexpectedException.Should().BeTrue();
        }
    }
}
