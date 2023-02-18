namespace WotPersonalDataCollector.Tests.Api.Services
{
	using System;
	using System.Net;
	using System.Net.Http;
	using System.Threading.Tasks;
	using WotPersonalDataCollector.Api.Http;
	using WotPersonalDataCollector.Api.Services;
	using WotPersonalDataCollector.Workflow;
	using static TddXt.AnyRoot.Root;

	[TestFixture]
    public class WotServiceTests
    {
        private IHttpClientWrapperFactory _httpClientWrapperFactory;
        private IWotService _uut;

        [SetUp]
        public void SetUp()
        {
            _httpClientWrapperFactory = Substitute.For<IHttpClientWrapperFactory>();
            _uut = new WotService(_httpClientWrapperFactory);
        }

        [Test]
        public async Task ShouldReturnHttpResponseMessage()
        {
            // Arrange
            var context = Any.Instance<WorkflowContext>();
            var responseMessage = Any.Instance<HttpResponseMessage>();
            responseMessage.StatusCode = HttpStatusCode.OK;
            _httpClientWrapperFactory.Create().PostAsync(context.UserInfoRequestMessage).Returns(responseMessage);

            // Act
            var actual = await _uut.GetUserApiResponseAsync(context.UserInfoRequestMessage);

            // Assert
            actual.Should().Be(responseMessage);
        }

        [Test]
        public async Task ShouldThrowExceptionWhenResponseNotOk()
        {
            // Arrange
            var context = Any.Instance<WorkflowContext>();
            var responseMessage = Any.Instance<HttpResponseMessage>();
            responseMessage.StatusCode = HttpStatusCode.NotFound;
            _httpClientWrapperFactory.Create().PostAsync(context.UserInfoRequestMessage).Returns(responseMessage);

            // Act
            Func<Task> act = async () => await _uut.GetUserApiResponseAsync(context.UserInfoRequestMessage);

            // Assert
            await act.Should().ThrowAsync<HttpRequestException>();
        }
    }
}
