using System;
using FluentAssertions;
using WotPersonalDataCollector.Utilities;
using NUnit.Framework;
using static TddXt.AnyRoot.Root;
using TddXt.AnyRoot.Strings;
using WotPersonalDataCollector.Exceptions;

namespace WotPersonalDataCollectorTests.Utilities
{
    [TestFixture]
    public class ConfigurationTests
    {
        private IConfiguration _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new Configuration();
        }

        [Test]
        public void ShouldReturnApplicationId()
        {
            // Arrange
            var applicationId = Any.String();
            Environment.SetEnvironmentVariable("ApplicationId", applicationId);

            // Act 
            var actual = _uut.ApplicationId;
            
            // Assert
            actual.Should().Be(applicationId);
        }

        [Test]
        public void ShouldReturnUserName()
        {
            // Arrange
            var userName = Any.String();
            Environment.SetEnvironmentVariable("WotUserName", userName);

            // Act 
            var actual = _uut.UserName;

            // Assert
            actual.Should().Be(userName);
        }

        [Test]
        public void ShouldReturnUserId()
        {
            // Arrange
            var userId = Any.String();
            Environment.SetEnvironmentVariable("UserId", userId);

            // Act 
            var actual = _uut.UserId;

            // Assert
            actual.Should().Be(userId);
        }

        [TestCase(null)]
        [TestCase("")]
        public void ShouldThrowExceptionWhenApplicationIdNullOrEmpty(string applicationId)
        {
            // Arrange 
            Environment.SetEnvironmentVariable("ApplicationId", applicationId);

            // Act
            Func<string> func = new Func<string>(() =>
            {
                return _uut.ApplicationId;
            });

            // Assert
            func.Should().Throw<LocalVariableException>().WithMessage("ApplicationId local variable is not set!");
        }

        [TestCase(null)]
        [TestCase("")]
        public void ShouldThrowExceptionWhenUserNameNullOrEmpty(string userName)
        {
            // Arrange 
            Environment.SetEnvironmentVariable("WotUserName", userName);

            // Act
            Func<string> func = new Func<string>(() =>
            {
                return _uut.UserName;
            });

            // Assert
            func.Should().Throw<LocalVariableException>().WithMessage("WotUserName local variable is not set!");
        }

        [TestCase(null)]
        [TestCase("")]
        public void ShouldThrowExceptionWhenUserIdNullOrEmpty(string userId)
        {
            // Arrange 
            Environment.SetEnvironmentVariable("UserId", userId);

            // Act
            Func<string> func = new Func<string>(() =>
            {
                return _uut.UserId;
            });

            // Assert
            func.Should().Throw<LocalVariableException>().WithMessage("UserId local variable is not set!");
        }

        [Test]
        public void ShouldReturnFalseWhenUserNameIsNull()
        {
            // Arrange
            Environment.SetEnvironmentVariable("WotUserName", null);

            // Act
            var actualBoolean = _uut.TryGetUserName(out var actualUserName);

            // Assert
            actualBoolean.Should().BeFalse();
            actualUserName.Should().BeNull();
        }

        [Test]
        public void ShouldReturnTrueWhenUserNameIsGiven()
        {
            // Arrange
            string userName = Any.String();
            Environment.SetEnvironmentVariable("WotUserName", userName);

            // Act
            var actualBoolean = _uut.TryGetUserName(out var actualUserName);

            // Assert
            actualBoolean.Should().BeTrue();
            actualUserName.Should().Be(userName);
        }

        [Test]
        public void ShouldSetUserIdAsLocalVariable()
        {
            // Arrange 
            string userId = Any.String();

            // Act
            Action act = () => _uut.UserId = userId;

            // Assert
            act.Should().NotThrow();
            var actual = _uut.UserId;
            actual.Should().Be(userId);
        }

        [TestCase(null)]
        [TestCase("")]
        public void ShouldThrowExceptionWhenSetNullOrEmptyUserId(string userId)
        {
            // Act
            Action act = () => _uut.UserId = userId;

            // Assert
            act.Should().Throw<LocalVariableException>().WithMessage("Provided userId can not be null or empty value!");
        }

    }
}