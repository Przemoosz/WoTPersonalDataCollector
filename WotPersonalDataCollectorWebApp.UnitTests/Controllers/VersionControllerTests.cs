using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using WotPersonalDataCollectorWebApp.Controllers;
using WotPersonalDataCollectorWebApp.CosmosDb.Context;
using WotPersonalDataCollectorWebApp.Services;
using WotPersonalDataCollectorWebApp.UnitTests.Categories;

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
		}

		[Test]
		public async Task ShouldRedirectToIndexWithMessageWhenCancellationIsNotAvailable()
		{
			// Arrange
			_validationCancellationService.IsCancellationAvailable.Returns(false);

			// Act
			var actual = await _uut.CancelValidationProcess();
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
		public async Task ShouldRedirectToIndexWithMessageWhenCancellationWasRequestedAgain()
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
			routeValueDictionary!["Message"].Should().Be("Operation cancellation has already started.");
		}

		[Test]
		public async Task ShouldRedirectToIndexWithMessageWhenTokenWasDisposed()
		{
			// Arrange
			_validationCancellationService.IsCancellationAvailable.Returns(true);
			_validationCancellationService.IsCancellationRequested.Returns(false);
			_validationCancellationService.IsTokenDisposed.Returns(true);

			// Act
			var actual = await _uut.CancelValidationProcess();
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
			var actual = await _uut.RequestValidationProcess(CancellationToken.None);
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
			var actual = await _uut.RequestValidationProcess(CancellationToken.None);
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
	}
}
