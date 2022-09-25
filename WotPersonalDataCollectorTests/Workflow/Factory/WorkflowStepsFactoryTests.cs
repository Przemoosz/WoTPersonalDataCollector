using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using WotPersonalDataCollector.Api;
using WotPersonalDataCollector.Api.Http;
using WotPersonalDataCollector.Api.Http.RequestObjects;
using WotPersonalDataCollector.Api.PersonalData;
using WotPersonalDataCollector.Api.Services;
using WotPersonalDataCollector.Api.User;
using WotPersonalDataCollector.CosmosDb.DTO;
using WotPersonalDataCollector.Workflow.Factory;
using WotPersonalDataCollector.Workflow.Steps;
using WotPersonalDataCollector.Workflow.Steps.Api;
using WotPersonalDataCollector.Workflow.Steps.Api.Http;
using WotPersonalDataCollector.Workflow.Steps.Api.Http.RequestObjects;
using WotPersonalDataCollector.Workflow.Steps.Api.PersonalData;
using WotPersonalDataCollector.Workflow.Steps.Api.Services;
using WotPersonalDataCollector.Workflow.Steps.Api.User;
using WotPersonalDataCollector.Workflow.Steps.CosmosDb;

namespace WotPersonalDataCollectorTests.Workflow.Factory
{
    [TestFixture]
    public class WorkflowStepsFactoryTests
    {
        private IWorkflowStepsFactory _uut;
        private IUserInfoRequestObjectFactory _userInfoRequestObjectFactory;
        private IUserRequestMessageFactory _userRequestMessagefactory;
        private IWotService _wotService;
        private IDeserializeUserIdHttpResponse _deserializeUserIdHttpResponse;
        private IApiUriFactory _apiUriFactory;
        private IUserPersonalDataRequestObjectFactory _userPersonalDataRequestObjectFactory;
        private IDeserializePersonalDataHttpResponse _deserializePersonalDataHttpResponse;
        private IWotDataCosmosDbDtoFactory _wotDataCosmosDbDtoFactory;

        [SetUp]
        public void SetUp()
        {
            _userInfoRequestObjectFactory = Substitute.For<IUserInfoRequestObjectFactory>();
            _userPersonalDataRequestObjectFactory = Substitute.For<IUserPersonalDataRequestObjectFactory>();
            _userRequestMessagefactory = Substitute.For<IUserRequestMessageFactory>();
            _wotService = Substitute.For<IWotService>();
            _apiUriFactory = Substitute.For<IApiUriFactory>();
            _deserializeUserIdHttpResponse = Substitute.For<IDeserializeUserIdHttpResponse>();
            _deserializePersonalDataHttpResponse = Substitute.For<IDeserializePersonalDataHttpResponse>();
            _wotDataCosmosDbDtoFactory = Substitute.For<IWotDataCosmosDbDtoFactory>();
            _uut = new WorkflowStepsFactory(_userInfoRequestObjectFactory, _userRequestMessagefactory, _wotService,
                _deserializeUserIdHttpResponse, _apiUriFactory, _userPersonalDataRequestObjectFactory,
                _deserializePersonalDataHttpResponse, _wotDataCosmosDbDtoFactory);
        }

        [Test]
        public void ShouldCreateUserInfoRequestIObjectStep()
        {
            // Act
            var actual = _uut.CreateUserInfoRequestObject();

            // Assert
            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<BaseStep>();
            actual.Should().BeOfType<CreateUserInfoRequestObjectStep>();
        }

        [Test]
        public void ShouldCreateUserInfoHttpRequestMessageStep()
        {
            // Act
            var actual = _uut.CreateUserInfoHttpRequestMessage();

            // Assert
            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<BaseStep>();
            actual.Should().BeOfType<UserInfoRequestMessageStep>();
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

        [Test]
        public void ShouldCreateUserPersonalDataRequestObjectStep()
        {
            // Act
            var actual = _uut.CreateUserPersonalDataRequestObject();

            // Assert
            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<BaseStep>();
            actual.Should().BeOfType<CreateUserPersonalDataRequestObjectStep>();
        }

        [Test]
        public void ShouldCreateUserPersonalDataHttpRequestMessageStep()
        {
            // Act
            var actual = _uut.CreateUserPersonalDataHttpRequestMessage();

            // Assert
            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<BaseStep>();
            actual.Should().BeOfType<UserPersonalDataRequestMessageStep>();
        }

        [Test]
        public void ShouldCreateUserInfoApiUriStep()
        {
            // Act
            var actual = _uut.CreateUserInfoApiUri();

            // Assert
            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<BaseStep>();
            actual.Should().BeOfType<CreateUserInfoApiUriStep>();
        }

        [Test]
        public void ShouldCreateUserPersonalDataUriStep()
        {
            // Act
            var actual = _uut.CreateUserPersonalDataApiUri();

            // Assert
            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<BaseStep>();
            actual.Should().BeOfType<CreateUserPersonalDataUriStep>();
        }

        [Test]
        public void ShouldCreateSendRequestForUserPersonalDataStep()
        {
            // Act
            var actual = _uut.CreateSendRequestForUserPersonalDataStep();

            // Assert
            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<BaseStep>();
            actual.Should().BeOfType<SendRequestForUserPersonalDataStep>();
        }

        [Test]
        public void ShouldCreateCreateWotApiResponseContractResolverStep()
        {
            // Act
            var actual = _uut.CreateWotApiResponseContractResolverStep();

            // Assert
            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<BaseStep>();
            actual.Should().BeOfType<CreateWotApiResponseContractResolverStep>();
        }

        [Test]
        public void ShouldCreateDeserializePersonalDataHttpResponseStep()
        {
            // Act
            var actual = _uut.CreateDeserializePersonalDataHttpResponseStep();

            // Assert
            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<BaseStep>();
            actual.Should().BeOfType<DeserializePersonalDataHttpResponseStep>();
        }

        [Test]
        public void ShouldCreateWotDataCosmosDbDtoCreateStep()
        {
            // Act
            var actual = _uut.CreateWotDataCosmosDbDtoCreateStep();

            // Assert
            actual.Should().NotBeNull();
            actual.Should().BeAssignableTo<BaseStep>();
            actual.Should().BeOfType<WotDataCosmosDbDtoCreateStep>();
        }
    }
}
