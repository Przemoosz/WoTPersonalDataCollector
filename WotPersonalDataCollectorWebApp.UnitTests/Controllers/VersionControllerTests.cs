using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MockQueryable.NSubstitute;
using NSubstitute;
using NUnit.Framework;
using WotPersonalDataCollectorWebApp.Controllers;
using WotPersonalDataCollectorWebApp.CosmosDb.Context;
using WotPersonalDataCollectorWebApp.Models;
using WotPersonalDataCollectorWebApp.Services;
using WotPersonalDataCollectorWebApp.UnitTests.Categories;
using WotPersonalDataCollectorWebApp.UnitTests.TestHelpers;
using static TddXt.AnyRoot.Root;

namespace WotPersonalDataCollectorWebApp.UnitTests.Controllers
{
	[TestFixture, ControllerTests, Parallelizable]
	public class VersionControllerTests
	{
		private ICosmosDatabaseContext _cosmosDatabaseContext = null!;
		private IValidationCancellationService _validationCancellationService = null!;
		private IVersionController _uut = null!;
		private IValidationService _validationService = null!;


		[SetUp]
		public void SetUp()
		{
			_cosmosDatabaseContext = Substitute.For<ICosmosDatabaseContext>();
			_validationCancellationService = Substitute.For<IValidationCancellationService>();
			_validationService = Substitute.For<IValidationService>();
			_uut = new VersionController(_cosmosDatabaseContext,_validationCancellationService, _validationService);
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
		public void ShouldActivateCancellationToken()
		{
			// Arrange
			_validationCancellationService.IsCancellationAvailable.Returns(true);
			_validationCancellationService.IsCancellationRequested.Returns(false);

			// Act
			var actual = _uut.CancelValidationProcess();
			var actualAsRedirectToAction = actual as RedirectToActionResult;

			// Assert
			_validationCancellationService.ReceivedWithAnyArgs(1).CancelValidation();
			actual.Should().BeOfType<RedirectToActionResult>();
			actualAsRedirectToAction!.ActionName.Should().Be("Index");
		}

		[Test]
		public void ShouldRedirectToIndexWithMessageWhenCancellationIsNotAvailable()
		{
			// Arrange
			_validationCancellationService.IsCancellationAvailable.Returns(false);

			// Act
			var actual = _uut.CancelValidationProcess();
			var actualAsRedirectToAction = actual as RedirectToActionResult;
			var routeValueDictionary = actualAsRedirectToAction?.RouteValues;

			// Assert
			actual.Should().BeOfType<RedirectToActionResult>();
			actualAsRedirectToAction.Should().NotBeNull();
			actualAsRedirectToAction?.ActionName?.Should().Be("Index");
			routeValueDictionary.Should().NotBeNull();
			routeValueDictionary!["Message"].Should().Be("Can not cancel operation that was not started or is finished!");
		}

		[Test]
		public void ShouldRedirectToIndexWithMessageWhenCancellationWasRequestedAgain()
		{
			// Arrange
			_validationCancellationService.IsCancellationAvailable.Returns(true);
			_validationCancellationService.IsCancellationRequested.Returns(true);

			// Act
			var actual = _uut.CancelValidationProcess();
			var actualAsRedirectToAction = actual as RedirectToActionResult;
			var routeValueDictionary = actualAsRedirectToAction?.RouteValues;

			// Assert
			actual.Should().BeOfType<RedirectToActionResult>();
			actualAsRedirectToAction.Should().NotBeNull();
			actualAsRedirectToAction?.ActionName?.Should().Be("Index");
			routeValueDictionary.Should().NotBeNull();
			routeValueDictionary!["Message"].Should().Be("Operation cancellation has already started.");
		}

		[Test]
		public void ShouldRedirectToIndexWithMessageWhenTokenWasDisposed()
		{
			// Arrange
			_validationCancellationService.IsCancellationAvailable.Returns(true);
			_validationCancellationService.IsCancellationRequested.Returns(false);
			_validationCancellationService.IsTokenDisposed.Returns(true);

			// Act
			var actual = _uut.CancelValidationProcess();
			var actualAsRedirectToAction = actual as RedirectToActionResult;
			var routeValueDictionary = actualAsRedirectToAction?.RouteValues;

			// Assert
			actual.Should().BeOfType<RedirectToActionResult>();
			actualAsRedirectToAction.Should().NotBeNull();
			actualAsRedirectToAction?.ActionName?.Should().Be("Index");
			routeValueDictionary.Should().NotBeNull();
			routeValueDictionary!["Message"].Should().Be("Cancellation token was disposed, that means validation operation is finished.");
		}

		[Test]
		public async Task ShouldRunValidationProcess()
		{
			// Arrange
			_validationService.IsValidationFinished.Returns(true);

			// Act
			var actual = _uut.RequestValidationProcess(CancellationToken.None);
			var actualAsRedirectToAction = actual as RedirectToActionResult;
			var routeValueDictionary = actualAsRedirectToAction?.RouteValues;

			// Assert
			await _validationService.Received(1).RunValidationProcessAsync();
			actual.Should().BeOfType<RedirectToActionResult>();
			actualAsRedirectToAction.Should().NotBeNull();
			actualAsRedirectToAction?.ActionName?.Should().Be("Index");
			routeValueDictionary!["IsCancellationEnabled"].Should().NotBeNull();
			((bool)routeValueDictionary["IsCancellationEnabled"]!).Should().BeTrue();
		}

		[Test]
		public async Task ShouldReturnIndexWithMessageIfValidationIsAlreadyRunning()
		{
			// Arrange
			_validationService.IsValidationFinished.Returns(false);

			// Act
			var actual = _uut.RequestValidationProcess(CancellationToken.None);
			var actualAsRedirectToAction = actual as RedirectToActionResult;
			var routeValueDictionary = actualAsRedirectToAction?.RouteValues;

			// Assert
			await _validationService.DidNotReceiveWithAnyArgs().RunValidationProcessAsync();
			actual.Should().BeOfType<RedirectToActionResult>();
			actualAsRedirectToAction.Should().NotBeNull();
			actualAsRedirectToAction?.ActionName?.Should().Be("Index");
			routeValueDictionary!["IsCancellationEnabled"].Should().NotBeNull();
			routeValueDictionary["Message"].Should().NotBeNull();
			((bool)routeValueDictionary["IsCancellationEnabled"]!).Should().BeTrue();
			routeValueDictionary["Message"].Should()
				.Be("Validation Operation has already started, can't start another one. Please wait.");
		}

		[Test]
		public void ShouldReturnsAllValidationResults()
		{
			// Arrange
			List<VersionValidateResultModel> validationResultModels = Any.Instance<List<VersionValidateResultModel>>();
			var dbSet = validationResultModels.AsDbSet();
			_cosmosDatabaseContext.VersionValidateResult.Returns(dbSet);

			// Act
			// TODO FIX TEST
			var actual = _uut.ValidationResults(1,"d");
			var viewResult = actual as ViewResult;
			
			// Assert
			viewResult.Should().NotBeNull();
			viewResult!.Model.Should().BeAssignableTo<IEnumerable<VersionValidateResultModel>>();
			var result = viewResult.Model as IEnumerable<VersionValidateResultModel>;
			var resultList = result!.ToList();
			resultList.Should().NotBeNull();
			resultList.Count.Should().Be(validationResultModels.Count);
		}

		[Test]
		public async Task ShouldReturnsLatestValidationResults()
		{
			// Arrange
			DateTime dt = DateTime.Now;
			List<VersionValidateResultModel> validationResultModels = new List<VersionValidateResultModel>(2)
			{
				new VersionValidateResultModel() { ValidationDate = dt },
				new VersionValidateResultModel() { ValidationDate = DateTime.MinValue }
			};
			var dbSet = validationResultModels.AsQueryable().BuildMockDbSet();
			_cosmosDatabaseContext.VersionValidateResult.Returns(dbSet);

			// Act
			var actual = await _uut.LatestValidationResult();
			var viewResult = actual as ViewResult;

			// Assert
			viewResult.Should().NotBeNull();
			viewResult!.Model.Should().BeAssignableTo<VersionValidateResultModel>();
			var result = viewResult.Model as VersionValidateResultModel;
			result.Should().NotBeNull();
			result!.ValidationDate.Should().Be(dt);
		}
	}
}
