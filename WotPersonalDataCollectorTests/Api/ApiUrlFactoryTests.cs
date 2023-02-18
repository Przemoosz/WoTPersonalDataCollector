namespace WotPersonalDataCollector.Tests.Api
{
	using TddXt.AnyRoot.Strings;
	using WotPersonalDataCollector.Api;
	using WotPersonalDataCollector.Api.Http.RequestObjects;
	using static TddXt.AnyRoot.Root;

	[TestFixture]
    public class ApiUrlFactoryTests
    {
        private ApiUriFactory _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new ApiUriFactory();
        }

        [Test]
        public void ShouldReturnApiUrlWithOneQuery()
        {
            // Arrange
            string url = Any.String();
            var requestObject = Any.Instance<IRequestObject>();

            // Act
            var actual = _uut.Create(url, requestObject);

            // Assert
            actual.Should().Be(url + "?" + "application_id=" + requestObject.application_id + "&");
        }

        [Test]
        public void ShouldReturnApiUrlWithTwoQuery()
        {
            // Arrange
            string url = Any.String();
            var requestObject = Any.Instance<UserInfoRequestObject>();
            
            // Act
            var actual = _uut.Create(url, requestObject);

            // Assert
            actual.Should().Be(url + "?" + "application_id=" + requestObject.application_id + "&search=" +
                               requestObject.search + "&");
        }
    }
}
