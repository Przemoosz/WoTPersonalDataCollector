namespace WotPersonalDataCollector.Tests.Api.Http.RequestObjects
{
	using TddXt.AnyRoot.Strings;
	using WotPersonalDataCollector.Api.Http.RequestObjects;
	using WotPersonalDataCollector.Utilities;
	using static TddXt.AnyRoot.Root;

	[TestFixture]
    public class UserInfoRequestObjectFactoryTests
    {
        private IConfiguration _configuration;
        private IUserInfoRequestObjectFactory _uut;

        [SetUp]
        public void SetUp()
        {
            _configuration = Substitute.For<IConfiguration>();
            _uut = new UserInfoRequestObjectFactory(_configuration);
        }

        [Test]
        public void ShouldReturnUserInfoRequestObject()
        {
            // Arrange
            var applicationId = Any.String();
            var userName = Any.String();
            _configuration.ApplicationId.Returns(applicationId);
            _configuration.TryGetUserName(out Arg.Any<string>()).Returns(x =>
            {
                x[0] = userName;
                return true;
            });

            // Act
            var actual = _uut.Create();

            // Assert
            actual.Should().BeAssignableTo<IRequestObject>();
            actual.Should().BeAssignableTo<UserInfoRequestObject>();
            var convertedActual = actual as UserInfoRequestObject;
            convertedActual.Should().NotBeNull();
            convertedActual!.search.Should().Be(userName);
            convertedActual!.application_id.Should().Be(applicationId);
        }
    }
}
