using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using WotPersonalDataCollectorWebApp.Controllers;
using WotPersonalDataCollectorWebApp.CosmosDb.Context;
using WotPersonalDataCollectorWebApp.CosmosDb.Dto;
using WotPersonalDataCollectorWebApp.CosmosDb.Dto.Metrics;
using WotPersonalDataCollectorWebApp.CosmosDb.Dto.Version;
using WotPersonalDataCollectorWebApp.Exceptions;
using WotPersonalDataCollectorWebApp.Services;
using WotPersonalDataCollectorWebApp.UnitTests.Categories;
using WotPersonalDataCollectorWebApp.UnitTests.TestUtilities;

namespace WotPersonalDataCollectorWebApp.UnitTests.Controllers
{
	[TestFixture, Parallelizable, ControllerTests]
	public class VersionControllerTests
	{
		private ICosmosDatabaseContext _cosmosDatabaseContext = null!;
		private IDtoVersionValidator _dtoVersionValidator = null!;
		private IValidationCancellationService _validationCancellationService = null!;
		private IVersionController _uut = null!;
		private readonly CancellationToken _nonCancelledCancellationToken = new CancellationToken(false);
		private const string ValidDataLabel = "ValidData";
		private const string ClassPropertiesWrongDataLabel = "ClassPropertiesWrongData";
		private const string WrongDataLabel = "WrongData";
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
			var routeValueDictionary = actualAsRedirectToAction?.RouteValues;

			// Assert
			actual.Should().BeOfType<RedirectToActionResult>();
			actualAsRedirectToAction.Should().NotBeNull();
			actualAsRedirectToAction?.ActionName?.Should().Be("Index");
			routeValueDictionary.Should().NotBeNull();
			routeValueDictionary!["Message"].Should().Be("Can not cancel operation that was not started!");


		}

		[Test]
		public async Task ShouldRedirectToIndexWithMessageWhenCancellationWasRequested()
		{
			// Arrange
			_validationCancellationService.IsCancellationAvailable.Returns(true);
			_validationCancellationService.IsCancellationRequested.Returns(true);

			// Act
			var actual = await _uut.CancelValidationProcess();
			var actualAsRedirectToAction = actual as RedirectToActionResult;
			var routeValueDictionary = actualAsRedirectToAction?.RouteValues;

			// Assert
			actual.Should().BeOfType<RedirectToActionResult>();
			actualAsRedirectToAction.Should().NotBeNull();
			actualAsRedirectToAction?.ActionName?.Should().Be("Index");
			routeValueDictionary.Should().NotBeNull();
			routeValueDictionary!["Message"].Should().Be("Operation cancellation is already started");
		}

		[TestCase(5)]
		[TestCase(0)]
		public async Task ShouldCorrectlyCountValidObjects(int itemsCount)
		{
			// Arrange
			var databaseCollection = new AsyncEnumerable<WotDataCosmosDbDto>(CreateValidData(itemsCount));
			SetUpMocksForRequestValidateProcess(databaseCollection);

			// Act
			var actual = await _uut.RequestValidationProcess(_nonCancelledCancellationToken);
			var actualAsRedirectToAction = actual as RedirectToActionResult;
			var routeValueDictionary = actualAsRedirectToAction?.RouteValues;

			// Assert
			actual.Should().BeOfType<RedirectToActionResult>();
			actualAsRedirectToAction.Should().NotBeNull();
			actualAsRedirectToAction?.ActionName?.Should().Be("ValidationResult");
			routeValueDictionary.Should().NotBeNull();
			routeValueDictionary!["TotalItemsInCosmosDb"].Should().Be(itemsCount);
			routeValueDictionary!["CorrectVersionDtoCount"].Should().Be(itemsCount);
			routeValueDictionary!["WasValidationCanceled"].Should().Be(false);
		}

		[Test]
		public async Task ShouldCorrectlyRecognizeObjectAsWrongForAllConditions()
		{
			// Arrange
			var databaseCollection = new AsyncEnumerable<WotDataCosmosDbDto>(CreateDataWithWrongClassProperties());
			SetUpMocksForRequestValidateProcess(databaseCollection);

			// Act
			var actual = await _uut.RequestValidationProcess(_nonCancelledCancellationToken);
			var actualAsRedirectToAction = actual as RedirectToActionResult;
			var routeValueDictionary = actualAsRedirectToAction?.RouteValues;

			// Assert
			actual.Should().BeOfType<RedirectToActionResult>();
			actualAsRedirectToAction.Should().NotBeNull();
			actualAsRedirectToAction?.ActionName?.Should().Be("ValidationResult");
			routeValueDictionary.Should().NotBeNull();
			routeValueDictionary!["TotalItemsInCosmosDb"].Should().Be(3);
			routeValueDictionary!["CorrectVersionDtoCount"].Should().Be(0);
			routeValueDictionary!["WrongObjectsCount"].Should().Be(3);
			routeValueDictionary!["WasValidationCanceled"].Should().Be(false);
		}

		[TestCase(2,3)]
		[TestCase(5, 0)]
		[TestCase(0, 7)]
		public async Task ShouldCorrectlyCountWrongObjectsWhenDtoVersionComponentsExceptionIsThrown(int validObjectsCount, int wrongObjectsCount)
		{
			// Arrange
			var databaseCollection = new AsyncEnumerable<WotDataCosmosDbDto>(CreateValidData(validObjectsCount), CreateWrongData(wrongObjectsCount));
			SetUpMocksForRequestValidateProcess(databaseCollection);

			// Act
			var actual = await _uut.RequestValidationProcess(_nonCancelledCancellationToken);
			var actualAsRedirectToAction = actual as RedirectToActionResult;
			var routeValueDictionary = actualAsRedirectToAction?.RouteValues;

			// Assert
			actual.Should().BeOfType<RedirectToActionResult>();
			actualAsRedirectToAction.Should().NotBeNull();
			actualAsRedirectToAction?.ActionName?.Should().Be("ValidationResult");
			routeValueDictionary.Should().NotBeNull();
			routeValueDictionary!["TotalItemsInCosmosDb"].Should().Be(validObjectsCount+wrongObjectsCount);
			routeValueDictionary!["CorrectVersionDtoCount"].Should().Be(validObjectsCount);
			routeValueDictionary!["WrongObjectsCount"].Should().Be(wrongObjectsCount);
			routeValueDictionary!["WasValidationCanceled"].Should().Be(false);
		}

		private void SetUpMocksForRequestValidateProcess(IAsyncEnumerable<WotDataCosmosDbDto> dataCollection)
		{
			_validationCancellationService.GetValidationCancellationToken(_nonCancelledCancellationToken)
				.ReturnsForAnyArgs(_nonCancelledCancellationToken);
			_cosmosDatabaseContext.PersonalData.AsAsyncEnumerable().Returns(dataCollection);
			_dtoVersionValidator.When(v => v.EnsureVersionCorrectness(Arg
				.Is<WotDataCosmosDbDto>(s => s.AccountId.Equals(WrongDataLabel)))).Do(t => throw new DtoVersionComponentsException());
		}

		private IEnumerable<WotDataCosmosDbDto> CreateValidData(int itemsCount)
		{
			var collection = new List<WotDataCosmosDbDto>(itemsCount);
			for (int i = 0; i < itemsCount; i++)
			{
				collection.Add(new WotDataCosmosDbDto(){ClassProperties = new ClassProperties(){Type = "WotAccount" , DtoVersion = "1.0.0"}, AccountId = ValidDataLabel});
			}
			return collection;
		}

		private IEnumerable<WotDataCosmosDbDto> CreateDataWithWrongClassProperties()
		{
			return new List<WotDataCosmosDbDto>(3)
			{
				new WotDataCosmosDbDto() { ClassProperties = null, AccountId = ClassPropertiesWrongDataLabel},
				new WotDataCosmosDbDto() { ClassProperties = new ClassProperties() { Type = "WrongType" }, AccountId = ClassPropertiesWrongDataLabel},
				new WotDataCosmosDbDto() { ClassProperties = new ClassProperties() { Type = "WotAccount", DtoVersion = null }, AccountId = ClassPropertiesWrongDataLabel }
			};
		}
		private IEnumerable<WotDataCosmosDbDto> CreateWrongData(int itemsCount)
		{
			var collection = new List<WotDataCosmosDbDto>(itemsCount);
			for (int i = 0; i < itemsCount; i++)
			{
				collection.Add(new WotDataCosmosDbDto() { ClassProperties = new ClassProperties() { Type = "WotAccount", DtoVersion = "1.0.0" }, AccountId = WrongDataLabel });
			}
			return collection;
		}

	}
}
