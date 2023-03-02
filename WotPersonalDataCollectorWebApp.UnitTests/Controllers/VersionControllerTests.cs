using WotPersonalDataCollector.WebApp.UnitTests.TestHelpers;

namespace WotPersonalDataCollector.WebApp.UnitTests.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using MockQueryable.NSubstitute;
	using TddXt.AnyRoot.Strings;
	using WotPersonalDataCollector.WebApp.Controllers;
	using WotPersonalDataCollector.WebApp.CosmosDb.Context;
	using Dto;
	using WotPersonalDataCollector.WebApp.Factories;
	using Models;
	using Properties;
	using WotPersonalDataCollector.WebApp.Services;
	using WotPersonalDataCollector.TestHelpers.Categories;
	using static TddXt.AnyRoot.Root;

	[TestFixture, ControllerTests, Parallelizable]
	public class VersionControllerTests
	{
		private ICosmosDatabaseContext _cosmosDatabaseContext = null!;
		private IValidationCancellationService _validationCancellationService = null!;
		private IVersionController _uut = null!;
		private IValidationService _validationService = null!;
		private IPageFactory<VersionValidateResultModel> _pageFactory = null!;
		private IResourcesWrapper _resourcesWrapper;


		[SetUp]
		public void SetUp()
		{
			_cosmosDatabaseContext = Substitute.For<ICosmosDatabaseContext>();
			_validationCancellationService = Substitute.For<IValidationCancellationService>();
			_validationService = Substitute.For<IValidationService>();
			_pageFactory = Substitute.For<IPageFactory<VersionValidateResultModel>>();
			_resourcesWrapper = Substitute.For<IResourcesWrapper>();
			_uut = new VersionController(_cosmosDatabaseContext, _validationCancellationService, _validationService,
				_pageFactory, _resourcesWrapper);
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
			var message = Any.String();
			_resourcesWrapper.CancelingCancelledOperationMessage.Returns(message);
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
			routeValueDictionary!["Message"].Should().Be(message);
		}

		[Test]
		public void ShouldRedirectToIndexWithMessageWhenCancellationWasRequestedAgain()
		{
			// Arrange
			var message = Any.String();
			_resourcesWrapper.CancellationOperationHasAlreadyStaredMessage.Returns(message);
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
			routeValueDictionary!["Message"].Should().Be(message);
		}

		[Test]
		public void ShouldRedirectToIndexWithMessageWhenTokenWasDisposed()
		{
			// Arrange
			var message = Any.String();
			_resourcesWrapper.CancellationTokenDisposedMessage.Returns(message);
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
			routeValueDictionary!["Message"].Should().Be(message);
		}

		[Test, Retry(3)]
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
			var message = Any.String();
			_resourcesWrapper.ValidationOperationIsAlreadyStartedMessage.Returns(message);
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
				.Be(message);
		}

		[TestCase("")]
		[TestCase("Ascending")]
		[TestCase("Descending")]
		public void ShouldReturnsDetailedPageOfValidationResults(string order)
		{
			// Arrange
			const int pageNumber = 1;
			List<VersionValidateResultModel> validationResultModels = Any.Instance<List<VersionValidateResultModel>>();
			var dbSet = validationResultModels.AsDbSet();
			var detailedPage = Any.Instance<DetailedPage<VersionValidateResultModel>>();
			_cosmosDatabaseContext.VersionValidateResult.Returns(dbSet);
			_pageFactory
				.CreateDetailedPage(Arg.Any<IEnumerable<VersionValidateResultModel>>(), Arg.Any<int>(), Arg.Any<int>())
				.Returns(detailedPage);
			// Act
			var actual = _uut.ValidationResults(pageNumber,order);
			var viewResult = actual as ViewResult;
			
			// Assert
			viewResult.Should().NotBeNull();
			viewResult!.Model.Should().BeAssignableTo<DetailedPage<VersionValidateResultModel>>();
			var result = viewResult.Model as DetailedPage<VersionValidateResultModel>;
			result.Should().NotBeNull();
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

		[Test]
		public async Task ShouldRemoveAllValidationResults()
		{
			// Arrange
			string message = Any.String();
			List<VersionValidateResultModel> validationResultModels = Any.Instance<List<VersionValidateResultModel>>();
			var dbSet = validationResultModels.AsDbSet();
			_cosmosDatabaseContext.VersionValidateResult.Returns(dbSet);
			_resourcesWrapper.DataDeleteProcessStarted.Returns(message);
			// Act
			var actual = await _uut.DeleteValidationData();
			var actualAsRedirectToAction = actual as RedirectToActionResult;
			var routeValueDictionary = actualAsRedirectToAction?.RouteValues;

			// Assert
			_cosmosDatabaseContext.VersionValidateResult.Received(validationResultModels.Count);
			await _cosmosDatabaseContext.Received(1).SaveChangesAsync();
			actual.Should().BeOfType<RedirectToActionResult>();
			actualAsRedirectToAction.Should().NotBeNull();
			actualAsRedirectToAction?.ActionName?.Should().Be("Index");
			routeValueDictionary.Should().NotBeNull();
			routeValueDictionary!["Message"].Should().Be(message);
		}
	}
}
