namespace WotPersonalDataCollector.Tests.CosmosDb.DatabaseContext
{
	using System.Threading.Tasks;
	using WotPersonalDataCollector.CosmosDb.DatabaseContext;
	using WotPersonalDataCollector.Utilities;

	[TestFixture]
    public class WotContextWrapperFactoryTests
    {
        private IConfiguration _configuration;
        private IWotContextWrapperFactory _uut;

        [SetUp]
        public void SetUp()
        {
            _configuration = Substitute.For<IConfiguration>();
            _uut = new WotContextWrapperFactory(_configuration);
        }

        [Test]
        public async Task ShouldReturnWotContextWrapper()
        {
            // Act
            await using (var actual = _uut.Create())
            {
                // Assert
                actual.Should().NotBeNull();
                actual.Should().BeOfType<WotContextWrapper>();
                actual.Should().BeAssignableTo<IWotContextWrapper>();
            }
        }
    }
}
