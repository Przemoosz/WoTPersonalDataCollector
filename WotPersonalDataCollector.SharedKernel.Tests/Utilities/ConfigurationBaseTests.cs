﻿namespace WotPersonalDataCollector.SharedKernel.Tests.Utilities
{
	using WotPersonalDataCollector.SharedKernel.Utilities;
	using TestHelpers;
	using System;
	using static TddXt.AnyRoot.Root;
	using TddXt.AnyRoot.Strings;
	using Exceptions;
	using Categories;

	[TestFixture, ConfigurationTests, Parallelizable]
	public class ConfigurationBaseTests
	{
		private ConfigurationBase _uut;

		[SetUp]
		public void SetUp()
		{
			_uut = new ConfigurationBaseImplementation();
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

		[Test]
		public void ShouldReturnWotDtoContainerName()
		{
			// Arrange
			var containerName = Any.String();
			Environment.SetEnvironmentVariable("WotDtoContainerName", containerName);

			// Act
			var actual = _uut.WotDtoContainerName;

			// Assert
			actual.Should().Be(containerName);
		}
		
		[Test]
		public void ShouldReturnCosmosConnectionString()
		{
			// Arrange
			var connectionString = Any.String();
			Environment.SetEnvironmentVariable("CosmosConnectionString", connectionString);

			// Act
			var actual = _uut.CosmosConnectionString;

			// Assert
			actual.Should().Be(connectionString);
		}
		[Test]
		public void ShouldReturnVersionModelContainerName()
		{
			// Arrange
			var versionModelContainerName = Any.String();
			Environment.SetEnvironmentVariable("VersionModelContainerName", versionModelContainerName);

			// Act
			var actual = _uut.VersionModelContainerName;

			// Assert
			actual.Should().Be(versionModelContainerName);
		}

		[Test]
		public void ShouldReturnVersionModelWotDtoVersion()
		{
			// Arrange
			var wotDtoVersion = Any.String();
			Environment.SetEnvironmentVariable("WotDtoVersion", wotDtoVersion);

			// Act
			var actual = _uut.WotDtoVersion;

			// Assert
			actual.Should().Be(wotDtoVersion);
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
			Environment.SetEnvironmentVariable("WotDtoContainerName", containerName);

			// Act
			Func<string> act = () => _uut.WotDtoContainerName;

			// Assert
			act.Should().Throw<LocalVariableException>().WithMessage("WotDtoContainerName local variable is not set!");
		}

		[TestCase(null)]
		[TestCase("")]
		public void ShouldThrowLocalVariableExceptionWhenVersionModelContainerNameIsNotSet(string versionModelContainerName)
		{
			// Arrange
			Environment.SetEnvironmentVariable("VersionModelContainerName", versionModelContainerName);

			// Act
			Func<string> act = () => _uut.VersionModelContainerName;

			// Assert
			act.Should().Throw<LocalVariableException>().WithMessage("VersionModelContainerName local variable is not set!");
		}

		[TestCase(null)]
		[TestCase("")]
		public void ShouldThrowLocalVariableExceptionWhenVersionModelWotDtoVersionIsNotSet(string wotDtoVersion)
		{
			// Arrange
			Environment.SetEnvironmentVariable("WotDtoVersion", wotDtoVersion);

			// Act
			Func<string> act = () => _uut.WotDtoVersion;

			// Assert
			act.Should().Throw<LocalVariableException>().WithMessage("WotDtoVersion local variable is not set!");
		}
	}
}
