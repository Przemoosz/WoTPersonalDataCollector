namespace WotPersonalDataCollector.Tests.CosmosDb.DTO
{
	using TddXt.AnyRoot.Strings;
	using WotPersonalDataCollector.Api.PersonalData.Dto;
	using WotPersonalDataCollector.Api.User.DTO;
	using WotPersonalDataCollector.CosmosDb.DTO;
	using WotPersonalDataCollector.Utilities;
	using static TddXt.AnyRoot.Root;

	[TestFixture]
    public class WotDataCosmosDbDtoFactoryTests
    {
        private IConfiguration _configuration;
        private IWotDataCosmosDbDtoFactory _uut;

        [SetUp]
        public void SetUp()
        {
            _configuration = Substitute.For<IConfiguration>();
            _uut = new WotDataCosmosDbDtoFactory(_configuration);
        }

        [Test]
        public void ShouldReturnWotDataCosmosDbDto()
        {
            // Arrange
            string dtoVersion = Any.String();
            WotAccountDto wotAccountDto = Any.Instance<WotAccountDto>();
            var userIdData = Any.Instance<UserIdData>();
            _configuration.WotDtoVersion.Returns(dtoVersion);
            
            // Act
            var actual = _uut.Create(wotAccountDto, userIdData);

            // Assert
            actual.AccountData.Should().Be(wotAccountDto);
            actual.ClassProperties.Type.Should().Be("WotAccount");
            actual.AccountId.Should().Be(userIdData.AccountId.ToString());
            actual.ClassProperties.DtoVersion.Should().Be(dtoVersion);
        }
    }
}
