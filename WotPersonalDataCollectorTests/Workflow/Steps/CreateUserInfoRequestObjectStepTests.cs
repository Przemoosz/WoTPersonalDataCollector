using System;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using WotPersonalDataCollector.Api.Http.RequestObjects;
using WotPersonalDataCollector.Workflow;
using WotPersonalDataCollector.Workflow.Steps.Api.Http;
using static TddXt.AnyRoot.Root;

namespace WotPersonalDataCollectorTests.Workflow.Steps
{
    [TestFixture]
    public class CreateUserInfoRequestObjectStepTests
    {
        private IUserInfoRequestObjectFactory _userInfoRequestObjectFactory;
        private CreateUserInfoRequestObjectStep _uut;

        [SetUp]
        public void SetUp()
        {
            _userInfoRequestObjectFactory = Substitute.For<IUserInfoRequestObjectFactory>();
            _uut = new CreateUserInfoRequestObjectStep(_userInfoRequestObjectFactory);
        }

        [Test]
        public async Task ShouldCreateUserInfoRequestObject()
        {
            // Act
            var context = Any.Instance<WorkflowContext>();
            var userInfoRequestObject = Any.Instance<UserInfoRequestObject>();
            _userInfoRequestObjectFactory.Create().Returns(userInfoRequestObject);

            // Act
            await _uut.ExecuteInner(context);

            // Assert
            context.UserInfoRequestObject.Should().Be(userInfoRequestObject);
        }

        [Test]
        public async Task ShouldSetSuccessfulStatusToFalseWhenExceptionIsThrown()
        {
            // Assert
            var context = Any.Instance<WorkflowContext>();
            _userInfoRequestObjectFactory.Create().Throws(new Exception());

            // Act
            await _uut.ExecuteInner(context);

            // Assert
            _uut.SuccessfulStatus().Should().BeFalse();
        }
    }
}
