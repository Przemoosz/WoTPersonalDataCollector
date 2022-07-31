using System;
using FluentAssertions;
using NSubstitute;
using WotPersonalDataCollector.Utilities;
using NUnit.Framework;
using static TddXt.AnyRoot.Root;
using TddXt.AnyRoot.Strings;
using WotPersonalDataCollector.Api.Http.RequestObjects;


namespace WotPersonalDataCollectorTests.Api.Http.RequestObjects
{
    [TestFixture]
    public class UserInfoRequestObjectFactoryTests
    {
        private IConfiguration _configuration;
        private IRequestObjectFactory _uut;

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
            // Environment.SetEnvironmentVariable("WotUserName", userName);
            _configuration.TryGetUserName(out Arg.Any<string>()).Returns(x =>
            {
                x[0] = userName;
                return true;
            });

            // Act
            var actual = _uut.Create();

            actual.Should().BeAssignableTo<IRequestObject>();
            actual.Should().BeAssignableTo<UserInfoRequestObject>();
            var convertedActual = actual as UserInfoRequestObject;
            convertedActual.Should().NotBeNull();
            convertedActual!.search.Should().Be(userName);
            convertedActual!.application_id.Should().Be(applicationId);
        }
    }
}
