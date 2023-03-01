namespace WotPersonalDataCollector.AzureMicroServicesFactory.Tests.Utilities
{
	using System;
	using TddXt.AnyRoot.Strings;
	using WotPersonalDataCollector.AzureMicroServicesFactory.Utilities;
	using SharedKernel.Exceptions;
	using static TddXt.AnyRoot.Root;

	[TestFixture, Parallelizable]
	public class MicroServicesConfigurationTests
	{
		private MicroServicesConfiguration _uut;

		[SetUp]
		public void Setup()
		{
			_uut = new MicroServicesConfiguration();
		}

		[Test]
		public void ShouldReturnAdminUsername()
		{
			// Arrange
			var adminUsername= Any.String();
			Environment.SetEnvironmentVariable("AdminUsername", adminUsername);

			// Act 
			var actual = _uut.AdminUsername;

			// Assert
			actual.Should().Be(adminUsername);
		}

		[TestCase(null)]
		[TestCase("")]
		public void ShouldThrowExceptionWhenAdminUsernameIsNullOrEmpty(string adminUsername)
		{
			// Arrange 
			Environment.SetEnvironmentVariable("AdminUsername", adminUsername);

			// Act
			Func<string> func = () => _uut.AdminUsername;

			// Assert
			func.Should().Throw<LocalVariableException>().WithMessage("AdminUsername local variable is not set!");
		}

		[Test]
		public void ShouldReturnAdminPassword()
		{
			// Arrange
			var adminPassword = Any.String();
			Environment.SetEnvironmentVariable("AdminPassword", adminPassword);

			// Act 
			var actual = _uut.AdminPassword;

			// Assert
			actual.Should().Be(adminPassword);
		}

		[TestCase(null)]
		[TestCase("")]
		public void ShouldThrowExceptionWheAdminPasswordIsNullOrEmpty(string adminPassword)
		{
			// Arrange 
			Environment.SetEnvironmentVariable("AdminPassword", adminPassword);

			// Act
			Func<string> func = () => _uut.AdminPassword;

			// Assert
			func.Should().Throw<LocalVariableException>().WithMessage("AdminPassword local variable is not set!");
		}
	}
}
