using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using WotPersonalDataCollector.Api;
using WotPersonalDataCollector.Api.Http;
using WotPersonalDataCollector.Api.Http.RequestObjects;
using WotPersonalDataCollector.Api.Services;
using WotPersonalDataCollector.Api.User;
using WotPersonalDataCollector.Workflow.Factory;
using WotPersonalDataCollector.Workflow.Steps;
using WotPersonalDataCollector.Workflow.Steps.Api.Http;
using WotPersonalDataCollector.Workflow.Steps.Api.Http.RequestObjects;
using WotPersonalDataCollector.Workflow.Steps.Api.Services;
using WotPersonalDataCollector.Workflow.Steps.Api.User;

namespace WotPersonalDataCollectorTests.Workflow.Factory
{
    [TestFixture]
    public class WorkflowStepsFactoryTests
    {
        private IWorkflowStepsFactory _uut;
        private IUserInfoRequestObjectFactory _userInfoRequestObjectFactory;
        private IUserInfoRequestMessageFactory _userInfoRequestMessagefactory;
        private IWotService _wotService;
        private IDeserializeUserIdHttpResponse _deserializeUserIdHttpResponse;
        private IApiUriFactory _apiUriFactory;
        private IUserPersonalDataRequestObjectFactory _userPersonalDataRequestObjectFactory;

        [SetUp]
        public void SetUp()
        {
            _userInfoRequestObjectFactory = Substitute.For<IUserInfoRequestObjectFactory>();
            _userPersonalDataRequestObjectFactory = Substitute.For<IUserPersonalDataRequestObjectFactory>();
            _userInfoRequestMessagefactory = Substitute.For<IUserInfoRequestMessageFactory>();
            _wotService = Substitute.For<IWotService>();
            _apiUriFactory = Substitute.For<IApiUriFactory>();
            _deserializeUserIdHttpResponse = Substitute.For<IDeserializeUserIdHttpResponse>();
            _uut = new WorkflowStepsFactory(_userInfoRequestObjectFactory, _userInfoRequestMessagefactory, _wotService,
                _deserializeUserIdHttpResponse, _apiUriFactory, _userPersonalDataRequestObjectFactory);
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
            var actual = _uut.CreateUserInfoHttpRequestMessage();

            // Assert
            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<BaseStep>();
            actual.Should().BeOfType<UserInfoRequestMessageCreateStep>();
        }

        [Test]
        public void ShouldCreateSendRequestForUserIdStep()
        {
            // Act
            var actual = _uut.CreateSendRequestForUserId();

            // Assert
            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<BaseStep>();
            actual.Should().BeOfType<SendRequestForUserIdStep>();
        }

        [Test]
        public void ShouldCreateDeserializeUserIdHttpResponseStep()
        {
            // Act
            var actual = _uut.CreateDeserializeUserIdResponseMessage();

            // Assert
            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<BaseStep>();
            actual.Should().BeOfType<DeserializeUserIdHttpResponseStep>();
        }
    }
}
