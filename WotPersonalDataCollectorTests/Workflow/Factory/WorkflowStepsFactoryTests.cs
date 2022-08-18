using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using WotPersonalDataCollector.Api.Http;
using WotPersonalDataCollector.Api.Http.RequestObjects;
using WotPersonalDataCollector.Api.Services;
using WotPersonalDataCollector.Workflow.Factory;
using WotPersonalDataCollector.Workflow.Steps;
using WotPersonalDataCollector.Workflow.Steps.Api.Http;
using WotPersonalDataCollector.Workflow.Steps.Api.Services;

namespace WotPersonalDataCollectorTests.Workflow.Factory
{
    [TestFixture]
    public class WorkflowStepsFactoryTests
    {
        private IWorkflowStepsFactory _uut;
        private IUserInfoRequestObjectFactory _userInfoRequestObjectFactory;
        private IHttpRequestMessageFactory _httpRequestMessagefactory;
        private IWotService _wotService;

        [SetUp]
        public void SetUp()
        {
            _userInfoRequestObjectFactory = Substitute.For<IUserInfoRequestObjectFactory>();
            _httpRequestMessagefactory = Substitute.For<IHttpRequestMessageFactory>();
            _wotService = Substitute.For<IWotService>();
            _uut = new WorkflowStepsFactory(_userInfoRequestObjectFactory, _httpRequestMessagefactory, _wotService);
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

        [Test]
        public void ShouldCreateHttpRequestMessageStep()
        {
            // Act
            var actual = _uut.CreateHttpRequestMessage();

            // Assert
            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<BaseStep>();
            actual.Should().BeOfType<HttpRequestMessageCreateStep>();
        }

        [Test]
        public void ShouldSendRequestForUserIdStep()
        {
            // Act
            var actual = _uut.CreateSendRequestForUserId();

            // Assert
            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<BaseStep>();
            actual.Should().BeOfType<SendRequestForUserIdStep>();
        }
    }
}
