namespace WotPersonalDataCollector.Tests.Api.Http
{
	using System;
	using System.Threading.Tasks;
	using WotPersonalDataCollector.Api.Http;

	[TestFixture]
    public class HttpClientWrapperTests
    {
        private IHttpClientWrapper _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new HttpClientWrapper();
        }

        [Test]
        public void ShouldThrowArgumentNullExceptionWhenRequestIsNull()
        {
            // Act
            Func<Task> act = async () =>
            {
                await _uut.PostAsync(null);
            };

            // Assert
            act.Should().ThrowAsync<ArgumentNullException>();
        }

        [TearDown]
        public void TearDown()
        {
            _uut.Dispose();
        }
    }
}