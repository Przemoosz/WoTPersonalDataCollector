using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using WotPersonalDataCollectorWebApp.Controllers;
using WotPersonalDataCollectorWebApp.CosmosDb.Context;
using WotPersonalDataCollectorWebApp.CosmosDb.Dto;
using WotPersonalDataCollectorWebApp.CosmosDb.Dto.Metrics;
using WotPersonalDataCollectorWebApp.CosmosDb.Dto.Version;
using WotPersonalDataCollectorWebApp.Services;
using WotPersonalDataCollectorWebApp.UnitTests.Categories;
using WotPersonalDataCollectorWebApp.UnitTests.TestUtilities;
using static TddXt.AnyRoot.Root;

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
			// Arrange
			_validationCancellationService.IsCancellationAvailable.Returns(true);
			_validationCancellationService.IsCancellationRequested.Returns(false);

			// Act
			var actual = await _uut.CancelValidationProcess();
			var actualAsRedirectToAction = actual as RedirectToActionResult;

			// Assert
			_validationCancellationService.ReceivedWithAnyArgs(1).CancelValidation();
			actual.Should().BeOfType<RedirectToActionResult>();
			actualAsRedirectToAction!.ActionName.Should().Be("Index");
			actualAsRedirectToAction.RouteValues.Should().BeNull();
		}

		[Test]
		public async Task ShouldRedirectToIndexWithMessageWhenCancellationIsNotAvailable()
		{
			// Arrange
			_validationCancellationService.IsCancellationAvailable.Returns(false);
			KeyValuePair<string, object?> expected =
				new KeyValuePair<string, object?>("Message", "Can not cancel operation that was not started!");
			// Act
			var actual = await _uut.CancelValidationProcess();
			var actualAsRedirectToAction = actual as RedirectToActionResult;
			var redirectValues = actualAsRedirectToAction?.RouteValues?.First();

			// Assert
			actual.Should().BeOfType<RedirectToActionResult>();
			actualAsRedirectToAction.Should().NotBeNull();
			actualAsRedirectToAction?.ActionName?.Should().Be("Index");
			redirectValues.Should().NotBeNull();
			redirectValues.Should().Be(expected);
			

		}

		[Test]
		public async Task ShouldRedirectToIndexWithMessageWhenCancellationWasRequested()
		{
			// Arrange
			_validationCancellationService.IsCancellationAvailable.Returns(true);
			_validationCancellationService.IsCancellationRequested.Returns(true);
			KeyValuePair<string, object?> expected =
				new KeyValuePair<string, object?>("Message", "Operation cancellation is already started");

			// Act
			var actual = await _uut.CancelValidationProcess();
			var actualAsRedirectToAction = actual as RedirectToActionResult;
			var redirectValues = actualAsRedirectToAction?.RouteValues?.First();

			// Assert
			actual.Should().BeOfType<RedirectToActionResult>();
			actualAsRedirectToAction.Should().NotBeNull();
			actualAsRedirectToAction?.ActionName?.Should().Be("Index");
			redirectValues.Should().NotBeNull();
			redirectValues.Should().Be(expected);
		}

		[TestCase(5)]
		public async Task ShouldCorrectlyValidateData(int itemsCount)
		{
			// Arrange
			CancellationToken nonCancelledCancellationToken = new CancellationToken(false);
			IAsyncEnumerable<WotDataCosmosDbDto> databaseCollection = CreateFakeDefaultData(itemsCount);
			_validationCancellationService.GetValidationCancellationToken(nonCancelledCancellationToken)
				.ReturnsForAnyArgs(nonCancelledCancellationToken);
			_cosmosDatabaseContext.PersonalData.AsAsyncEnumerable().Returns(databaseCollection);

			// Act
			var actual = await _uut.RequestValidationProcess(nonCancelledCancellationToken);
			var actualAsRedirectToAction = actual as RedirectToActionResult;
			var routeValueDictionary = actualAsRedirectToAction?.RouteValues;

			// Assert
			actual.Should().BeOfType<RedirectToActionResult>();
			actualAsRedirectToAction.Should().NotBeNull();
			actualAsRedirectToAction?.ActionName?.Should().Be("ValidationResult");
			routeValueDictionary.Should().NotBeNull();
			routeValueDictionary!["TotalItemsInCosmosDb"].Should().Be(itemsCount);
			routeValueDictionary!["CorrectVersionDtoCount"].Should().Be(itemsCount);
			routeValueDictionary["WasValidationCanceled"].Should().Be(false);

		}

		private IAsyncEnumerable<WotDataCosmosDbDto> CreateFakeDefaultData(int itemsCount)
		{
			var collection = new AsyncEnumerable<WotDataCosmosDbDto>(itemsCount);
			for (int i = 0; i < itemsCount; i++)
			{
				collection.Add(new WotDataCosmosDbDto(){ClassProperties = new ClassProperties(){Type = "WotAccount" , DtoVersion = "1.0.0"} });
			}
			return collection;
		}
	}
}
