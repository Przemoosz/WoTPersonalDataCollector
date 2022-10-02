using System;
using FluentAssertions;
using WotPersonalDataCollector.Utilities;
using NUnit.Framework;
using TddXt.AnyRoot.Math;
using TddXt.AnyRoot.Numbers;
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

        [Test]
        public void ShouldReturnPlayersUri()
        {
            // Arrange
            var playersUri = Any.String();
            Environment.SetEnvironmentVariable("PlayersUri", playersUri);

            // Act 
            var actual = _uut.PlayersUri;

            // Assert
            actual.Should().Be(playersUri);
        }

        [Test]
        public void ShouldReturnPersonalDataUri()
        {
            // Arrange
            var personalDataUri = Any.String();
            Environment.SetEnvironmentVariable("PersonalDataUri", personalDataUri);

            // Act 
            var actual = _uut.PersonalDataUri;

            // Assert
            actual.Should().Be(personalDataUri);
        }

        [Test]
        public void ShouldReturnCosmosConnectionString()
        {
            // Arrange
            var cosmosConnectionString = Any.String();
            Environment.SetEnvironmentVariable("CosmosConnectionString", cosmosConnectionString);

            // Act 
            var actual = _uut.CosmosConnectionString;

            // Assert
            actual.Should().Be(cosmosConnectionString);
        }

        [Test]
        public void ShouldReturnCosmosDbName()
        {
            // Arrange
            var cosmosDbName = Any.String();
            Environment.SetEnvironmentVariable("CosmosDbName", cosmosDbName);

            // Act 
            var actual = _uut.CosmosDbName;

            // Assert
            actual.Should().Be(cosmosDbName);
        }

        [Test]
        public void ShouldReturnDatabaseThroughput()
        {
            // Arrange
            var databaseThroughput = Any.IntegerWithExactDigitsCount(4);
            Environment.SetEnvironmentVariable("DatabaseThroughput", databaseThroughput.ToString());

            // Act 
            var actual = _uut.DatabaseThroughput;

            // Assert
            actual.Should().Be(databaseThroughput);
        }

        [Test]
        public void ShouldReturnContainerName()
        {
            // Arrange
            var containerName = Any.String();
            Environment.SetEnvironmentVariable("ContainerName", containerName);

            // Act 
            var actual = _uut.ContainerName;

            // Assert
            actual.Should().Be(containerName);
        }

        [Test]
        public void ShouldReturnDtoVersion()
        {
            // Arrange
            var dtoVersion = Any.String();
            Environment.SetEnvironmentVariable("DtoVersion", dtoVersion);

            // Act 
            var actual = _uut.DtoVersion;

            // Assert
            actual.Should().Be(dtoVersion);
        }

        [Test]
        public void ShouldThrowExceptionWhenDatabaseThroughputIsLowerThanFourHundred()
        {
            // Arrange
            var databaseThroughput = Any.IntegerWithExactDigitsCount(2);
            Environment.SetEnvironmentVariable("DatabaseThroughput", databaseThroughput.ToString());

            // Act 
            Func<int> act = () => _uut.DatabaseThroughput;

            // Assert
            act.Should().Throw<DatabaseThroughputException>()
                .WithMessage("DatabaseThroughput can not be lower than 400!");
        }

        [Test]
        public void ShouldThrowExceptionWhenDatabaseThroughputIsNotIntType()
        {
            // Arrange
            var databaseThroughput = Any.String();
            Environment.SetEnvironmentVariable("DatabaseThroughput", databaseThroughput);

            // Act 
            Func<int> act = () => _uut.DatabaseThroughput;

            // Assert
            act.Should().Throw<DatabaseThroughputException>()
                .WithMessage("Can not parse provided throughput in local variables to Int32!");
        }

        [TestCase(null)]
        [TestCase("")]
        public void ShouldThrowExceptionWhenCosmosDbNameIsNullOrEmpty(string cosmosDbName)
        {
            // Arrange 
            Environment.SetEnvironmentVariable("CosmosDbName", cosmosDbName);

            // Act
            Func<string> func = () => _uut.CosmosDbName;

            // Assert
            func.Should().Throw<LocalVariableException>().WithMessage("CosmosDbName local variable is not set!");
        }

        [TestCase(null)]
        [TestCase("")]
        public void ShouldThrowExceptionWhenDatabaseThroughputIsNullOrEmpty(string databaseThroughput)
        {
            // Arrange 
            Environment.SetEnvironmentVariable("DatabaseThroughput", databaseThroughput);

            // Act
            Func<int> func = () => _uut.DatabaseThroughput;

            // Assert
            func.Should().Throw<LocalVariableException>().WithMessage("DatabaseThroughput local variable is not set!");
        }

        [TestCase(null)]
        [TestCase("")]
        public void ShouldThrowExceptionWhenApplicationIdIsNullOrEmpty(string applicationId)
        {
            // Arrange 
            Environment.SetEnvironmentVariable("ApplicationId", applicationId);

            // Act
            Func<string> func = () => _uut.ApplicationId;

            // Assert
            func.Should().Throw<LocalVariableException>().WithMessage("ApplicationId local variable is not set!");
        }

        [TestCase(null)]
        [TestCase("")]
        public void ShouldThrowExceptionWhenCosmosConnectionStringIsNullOrEmpty(string cosmosConnectionString)
        {
            // Arrange 
            Environment.SetEnvironmentVariable("CosmosConnectionString", cosmosConnectionString);

            // Act
            Func<string> func = () => _uut.CosmosConnectionString;

            // Assert
            func.Should().Throw<LocalVariableException>().WithMessage("CosmosConnectionString local variable is not set!");
        }

        [TestCase(null)]
        [TestCase("")]
        public void ShouldThrowExceptionWhenUserNameIsNullOrEmpty(string userName)
        {
            // Arrange 
            Environment.SetEnvironmentVariable("WotUserName", userName);

            // Act
            Func<string> func = () => _uut.UserName;

            // Assert
            func.Should().Throw<LocalVariableException>().WithMessage("WotUserName local variable is not set!");
        }

        [TestCase(null)]
        [TestCase("")]
        public void ShouldThrowExceptionWhenUserIdIsNullOrEmpty(string userId)
        {
            // Arrange 
            Environment.SetEnvironmentVariable("UserId", userId);

            // Act
            Func<string> func = () => _uut.UserId;

            // Assert
            func.Should().Throw<LocalVariableException>().WithMessage("UserId local variable is not set!");
        }

        [TestCase(null)]
        [TestCase("")]
        public void ShouldThrowExceptionWhenPersonalDataUriIsNullOrEmpty(string personalDataUri)
        {
            // Arrange 
            Environment.SetEnvironmentVariable("PersonalDataUri", personalDataUri);

            // Act
            Func<string> func = () => _uut.PersonalDataUri;

            // Assert
            func.Should().Throw<LocalVariableException>().WithMessage("PersonalDataUri local variable is not set!");
        }

        [TestCase(null)]
        [TestCase("")]
        public void ShouldThrowExceptionWhenPlayersUriIsNullOrEmpty(string playersUri)
        {
            // Arrange 
            Environment.SetEnvironmentVariable("PlayersUri", playersUri);

            // Act
            Func<string> func = () => _uut.PlayersUri;

            // Assert
            func.Should().Throw<LocalVariableException>().WithMessage("PlayersUri local variable is not set!");
        }

        [TestCase(null)]
        [TestCase("")]
        public void ShouldThrowExceptionWhenContainerNameIsNullOrEmpty(string containerName)
        {
            // Arrange 
            Environment.SetEnvironmentVariable("ContainerName", containerName);

            // Act
            Func<string> func = () => _uut.ContainerName;

            // Assert
            func.Should().Throw<LocalVariableException>().WithMessage("ContainerName local variable is not set!");
        }

        [TestCase(null)]
        [TestCase("")]
        public void ShouldThrowExceptionWhenDtoVersionIsNullOrEmpty(string dtoVersion)
        {
            // Arrange 
            Environment.SetEnvironmentVariable("DtoVersion", dtoVersion);

            // Act
            Func<string> func = () => _uut.DtoVersion;

            // Assert
            func.Should().Throw<LocalVariableException>().WithMessage("DtoVersion local variable is not set!");
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