using FluentAssertions;
using Microsoft.AspNetCore.Mvc.WebApiCompatShim;
using NSubstitute;
using NUnit.Framework;
using WotPersonalDataCollector.Api.Http;
using WotPersonalDataCollector.Api.Http.RequestObjects;
using WotPersonalDataCollector.Workflow.Factory;
using WotPersonalDataCollector.Workflow.Steps;
using WotPersonalDataCollector.Workflow.Steps.Api.Http;

namespace WotPersonalDataCollectorTests.Workflow.Factory
{
    [TestFixture]
    public class WorkflowStepsFactoryTests
    {
        private IWorkflowStepsFactory _uut;
        private IUserInfoRequestObjectFactory _userInfoRequestObjectFactory;
        private IHttpRequestMessageFactory _httpRequestMessagefactory;

        [SetUp]
        public void SetUp()
        {
            _userInfoRequestObjectFactory = Substitute.For<IUserInfoRequestObjectFactory>();
            _httpRequestMessagefactory = Substitute.For<IHttpRequestMessageFactory>();
            _uut = new WorkflowStepsFactory(_userInfoRequestObjectFactory, _httpRequestMessagefactory);
        }

        [Test]
        public void ShouldCreateUserRequestInfoObjectStep()
        {
            // Act
            var actual = _uut.CreateUserInfoRequestObject();

            // Assert
            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<BaseStep>();
            actual.Should().BeOfType<CreateUserInfoRequestObjectStep>();
        }
    }
}
