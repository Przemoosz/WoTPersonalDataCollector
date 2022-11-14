using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using WotPersonalDataCollectorWebApp.Controllers;
using WotPersonalDataCollectorWebApp.CosmosDb.Context;
using WotPersonalDataCollectorWebApp.CosmosDb.Dto.Version;
using WotPersonalDataCollectorWebApp.Services;
using WotPersonalDataCollectorWebApp.UnitTests.Categories;

namespace WotPersonalDataCollectorWebApp.UnitTests.Controllers
{
	[TestFixture, Parallelizable, ControllerTests]
	public class VersionControllerTests
	{
		private ICosmosDatabaseContext _cosmosDatabaseContext = null!;
		private IDtoVersionValidator _dtoVersionValidator = null!;
		private IValidationCancellationService _validationCancellationService = null!;
		private IVersionController _uut = null!;


		[SetUp]
		public void SetUp()
		{
			_cosmosDatabaseContext = Substitute.For<ICosmosDatabaseContext>();
			_dtoVersionValidator = Substitute.For<IDtoVersionValidator>();
			_validationCancellationService = Substitute.For<IValidationCancellationService>();
			_uut = new VersionController(_cosmosDatabaseContext,_dtoVersionValidator,_validationCancellationService);
		}

		[Test]
		public void ShouldReturnIndexView()
		{
			// Act
			var actual = _uut.Index();
			var actualViewResult = actual as ViewResult;

			// Assert
			actual.Should().BeOfType<ViewResult>();
			actualViewResult!.ViewData.Count.Should().Be(0);
		}

		[Test]
		public async Task ShouldActivateCancellationToken()
		{
			// Act
			var actual = await _uut.CancelValidationProcess();

			// Assert
			_validationCancellationService.ReceivedWithAnyArgs(1).CancelValidation();
			actual.Should().BeOfType<ViewResult>();
		}

	}
}
