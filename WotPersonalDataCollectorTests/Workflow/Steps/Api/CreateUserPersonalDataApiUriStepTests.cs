namespace WotPersonalDataCollector.Tests.Workflow.Steps.Api
{
	using System.Threading.Tasks;
	using NSubstitute.ExceptionExtensions;
	using TddXt.AnyRoot;
	using TddXt.AnyRoot.Strings;
	using WotPersonalDataCollector.Api;
	using WotPersonalDataCollector.Workflow;
	using WotPersonalDataCollector.Workflow.Steps.Api;
	using static TddXt.AnyRoot.Root;

	[TestFixture]
    public class CreateUserPersonalDataApiUriStepTests
    {
        private IApiUriFactory _apiUriFactory;
        private CreateUserPersonalDataUriStep _uut;

        [SetUp]
        public void SetUp()
        {
            _apiUriFactory = Substitute.For<IApiUriFactory>();
            _uut = new CreateUserPersonalDataUriStep(_apiUriFactory);
        }

        [Test]
        public async Task ShouldCreateUserPersonalDataApiUri()
        {
            // Arrange
            var context = Any.Instance<WorkflowContext>();
            context.UserPersonalDataApiUrlWithParameters = null;
            var apiUri = Any.String();
            _apiUriFactory.Create(context.UserPersonalDataApiUrl, context.UserPersonalDataRequestObject).Returns(apiUri);

            // Act
            await _uut.ExecuteInner(context);

            // Assert
            context.UserPersonalDataApiUrlWithParameters.Should().NotBeNull();
            context.UserPersonalDataApiUrlWithParameters.Should().Be(apiUri);
            _uut.SuccessfulStatus().Should().BeTrue();
        }

        [Test]
        public async Task ShouldSetSuccessfulStatusToFalseWhenAnyExceptionIsThrown()
        {
            // Arrange
            var context = Any.Instance<WorkflowContext>();
            context.UserPersonalDataApiUrlWithParameters = null;
            _apiUriFactory.Create(context.UserPersonalDataApiUrl, context.UserPersonalDataRequestObject).Throws(Any.Exception());

            // Act
            await _uut.ExecuteInner(context);

            // Assert
            _uut.SuccessfulStatus().Should().BeFalse();
            context.UserPersonalDataApiUrlWithParameters.Should().BeNull();
            context.UnexpectedException.Should().BeTrue();
        }
    }
}
