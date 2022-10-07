using FluentAssertions;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using System;
using TddXt.AnyRoot.Strings;
using WotPersonalDataCollectorWebApp.Exceptions;
using WotPersonalDataCollectorWebApp.Utilities;
using static TddXt.AnyRoot.Root;

namespace WotPersonalDataCollectorWebApp.UnitTests.Utilities
{
    [TestFixture]
    public class AspConfigurationTests
    {
        private IAspConfiguration _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new AspConfiguration();
        }

        [Test]
        public void ShouldReturnCosmosConnectionString()
        {
            // Arrange
            var connectionString = Any.String(139);
            Environment.SetEnvironmentVariable("CosmosConnectionString", connectionString);

            // Act
            var actual = _uut.CosmosConnectionString;

            // Assert
            actual.Should().Be(connectionString);
        }

        [Test]
        public void ShouldReturnContainerName()
        {
            // Arrange
            var containerName = Any.String(139);
            Environment.SetEnvironmentVariable("ContainerName", containerName);

            // Act
            var actual = _uut.ContainerName;

            // Assert
            actual.Should().Be(containerName);
        }

        [Test]
        public void ShouldReturnDatabaseName()
        {
            // Arrange
            var databaseName = Any.String();
            Environment.SetEnvironmentVariable("DatabaseName", databaseName);

            // Act
            var actual = _uut.DatabaseName;

            // Assert
            actual.Should().Be(databaseName);
        }

        [TestCase(null)]
        [TestCase("")]
        public void ShouldThrowLocalVariableExceptionWhenConnectionStringIsNotSet(string connectionString)
        {
            // Arrange
            Environment.SetEnvironmentVariable("CosmosConnectionString", connectionString);

            // Act
            Func<string> act = () => _uut.CosmosConnectionString;

            // Assert
            act.Should().Throw<LocalVariableException>().WithMessage("CosmosConnectionString local variable is not set!");
        }

        [TestCase(null)]
        [TestCase("")]
        public void ShouldThrowLocalVariableExceptionWhenDatabaseNameIsNotSet(string databaseName)
        {
            // Arrange
            Environment.SetEnvironmentVariable("DatabaseName", databaseName);

            // Act
            Func<string> act = () => _uut.DatabaseName;

            // Assert
            act.Should().Throw<LocalVariableException>().WithMessage("DatabaseName local variable is not set!");
        }

        [TestCase(null)]
        [TestCase("")]
        public void ShouldThrowLocalVariableExceptionWhenContainerNameIsNotSet(string containerName)
        {
            // Arrange
            Environment.SetEnvironmentVariable("ContainerName", containerName);

            // Act
            Func<string> act = () => _uut.ContainerName;

            // Assert
            act.Should().Throw<LocalVariableException>().WithMessage("ContainerName local variable is not set!");
        }

        [TestCase(180)]
        [TestCase(4)]
        public void ShouldThrowLocalVariableExceptionWhenConnectionStringLengthIsNotValid(int length)
        {
            // Arrange
            var connectionString = Any.String(length);
            Environment.SetEnvironmentVariable("CosmosConnectionString", connectionString);

            // Act
            Func<string> act = () => _uut.CosmosConnectionString;

            // Assert
            act.Should().Throw<LocalVariableException>().WithMessage("Connection string length is not valid. 139 chars connection string is required!");
        }
    }
}
