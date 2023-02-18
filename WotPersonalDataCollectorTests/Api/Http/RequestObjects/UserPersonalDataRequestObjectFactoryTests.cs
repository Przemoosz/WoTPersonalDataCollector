using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using TddXt.AnyRoot.Strings;
using WotPersonalDataCollector.Api.Http.RequestObjects;
using WotPersonalDataCollector.Api.User.DTO;
using WotPersonalDataCollector.Utilities;
using static TddXt.AnyRoot.Root;

namespace WotPersonalDataCollector.Tests.Api.Http.RequestObjects
{
    [TestFixture]
    public class UserPersonalDataRequestObjectFactoryTests
    {
        private IConfiguration _configuration;
        private IUserPersonalDataRequestObjectFactory _uut;

        [SetUp]
        public void SetUp()
        {
            _configuration = Substitute.For<IConfiguration>();
            _uut = new UserPersonalDataRequestObjectFactory(_configuration);
        }

        [Test]
        public void ShouldUserPersonalDataRequestObject()
        {
            // Arrange
            var applicationId = Any.String();
            var userIdData = Any.Instance<UserIdData>();
            _configuration.ApplicationId.Returns(applicationId);

            // Act
            var actual = _uut.Create(userIdData);

            // Assert
            actual.Should().BeAssignableTo<IRequestObject>();
            actual.Should().BeAssignableTo<UserPersonalDataRequestObject>();
            var convertedActual = actual as UserPersonalDataRequestObject;
            convertedActual.Should().NotBeNull();
            convertedActual!.application_id.Should().Be(applicationId);
            convertedActual!.account_id.Should().Be(userIdData.AccountId.ToString());
        }
    }
}
