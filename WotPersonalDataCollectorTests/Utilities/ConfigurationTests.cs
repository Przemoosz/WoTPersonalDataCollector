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
            Environment.SetEnvironmentVariable("UserName", userName);

            // Act 
            var actual = _uut.UserName;

            // Assert
            actual.Should().Be(userName);
        }

        [TestCase(null)]
        [TestCase("")]
        public void ShouldThrowExceptionWhenApplicationIdNullOrEmpty(string applicationId)
        {
            // Arrange 
            Environment.SetEnvironmentVariable("ApplicationId", applicationId);

            // Act
            Action act = new Action(() =>
            {
                var a = _uut.ApplicationId;
            });
            
            // Assert
            act.Should().Throw<LocalVariableException>().WithMessage("ApplicationId local variable is not set!");
        }

        [TestCase(null)]
        [TestCase("")]
        public void ShouldThrowExceptionWhenUserNameNullOrEmpty(string userName)
        {
            // Arrange 
            Environment.SetEnvironmentVariable("UserName", userName);

            // Act
            Action act = new Action(() =>
            {
                var a = _uut.UserName;
            });

            // Assert
            act.Should().Throw<LocalVariableException>().WithMessage("UserName local variable is not set!");
        }
    }
}