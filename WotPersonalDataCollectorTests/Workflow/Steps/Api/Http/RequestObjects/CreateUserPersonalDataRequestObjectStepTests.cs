using System;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using WotPersonalDataCollector.Api.Http.RequestObjects;
using WotPersonalDataCollector.Workflow;
using WotPersonalDataCollector.Workflow.Steps.Api.Http.RequestObjects;
using static TddXt.AnyRoot.Root;

namespace WotPersonalDataCollectorTests.Workflow.Steps.Api.Http.RequestObjects
{
    [TestFixture]
    public class CreateUserPersonalDataRequestObjectStepTests
    {
        private CreateUserPersonalDataRequestObjectStep _uut;
        private IUserPersonalDataRequestObjectFactory _userPersonalDataRequestObjectFactory;

        [SetUp]
        public void SetUp()
        {
            _userPersonalDataRequestObjectFactory = Substitute.For<IUserPersonalDataRequestObjectFactory>();
            _uut = new CreateUserPersonalDataRequestObjectStep(_userPersonalDataRequestObjectFactory);
        }

        [Test]
        public async Task ShouldCreateUserInfoRequestObject()
        {
            // Arrange
            var context = Any.Instance<WorkflowContext>();
            context.UserPersonalDataRequestObject = null;
            var userPersonalDataRequestObject = Any.Instance<UserPersonalDataRequestObject>();
            _userPersonalDataRequestObjectFactory.Create(context.UserIdData).Returns(userPersonalDataRequestObject);

            // Act
            await _uut.ExecuteInner(context);

            // Assert
            context.UserPersonalDataRequestObject.Should().NotBeNull();
            context.UserPersonalDataRequestObject.Should().Be(userPersonalDataRequestObject);
            _uut.SuccessfulStatus().Should().BeTrue();
        }

        [Test]
        public async Task ShouldSetSuccessfulStatusToFalseWhenAnyExceptionIsThrown()
        {
            // Arrange
            var context = Any.Instance<WorkflowContext>();
            context.UserPersonalDataRequestObject = null;
            context.UnexpectedException = false;
            _userPersonalDataRequestObjectFactory.Create(context.UserIdData).Throws(new Exception());

            // Act
            await _uut.ExecuteInner(context);

            // Assert
            _uut.SuccessfulStatus().Should().BeFalse();
            context.UserPersonalDataRequestObject.Should().BeNull();
            context.UnexpectedException.Should().BeTrue();
        }
    }
}
