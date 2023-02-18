using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WotPersonalDataCollector.WebApp.CosmosDb.Context;
using WotPersonalDataCollector.WebApp.Extensions;
using WotPersonalDataCollector.WebApp.Factories;
using WotPersonalDataCollector.WebApp.Models;
using WotPersonalDataCollector.WebApp.Models.ViewModels;
using WotPersonalDataCollector.WebApp.Properties;
using WotPersonalDataCollector.WebApp.Services;

namespace WotPersonalDataCollector.WebApp.Controllers
{
	public sealed class VersionController: Controller, IVersionController
	{
		private const string Ascending = "Ascending";
		private const string Descending = "Descending";
		private const int PageSize = 5;
		private readonly ICosmosDatabaseContext _context;
		private readonly IValidationCancellationService _validationCancellationService;
		private readonly IValidationService _validationService;
		private readonly IPageFactory<VersionValidateResultModel> _pageFactory;
		private readonly IResourcesWrapper _resourcesWrapper;


		public VersionController(ICosmosDatabaseContext context,
			IValidationCancellationService validationCancellationService, IValidationService validationService,
			IPageFactory<VersionValidateResultModel> pageFactory, IResourcesWrapper resourcesWrapper)
		{
			_context = context;
			_validationCancellationService = validationCancellationService;
			_validationService = validationService;
			_pageFactory = pageFactory;
			_resourcesWrapper = resourcesWrapper;
		}

		public IActionResult Index(VersionValidateViewModel viewModel = null)
		{
			return View(viewModel);
		}

		[HttpGet]
		public IActionResult RequestValidationProcess(CancellationToken token)
		{
			if (!_validationService.IsValidationFinished)
			{
				return RedirectToAction(nameof(Index),
					new VersionValidateViewModel()
					{
						IsCancellationEnabled = true,
						Message = _resourcesWrapper.ValidationOperationIsAlreadyStartedMessage
					});
			}

			_validationCancellationService.GetValidationCancellationToken(token);
			ThreadPool.QueueUserWorkItem(s => _validationService.RunValidationProcessAsync());
			return RedirectToAction(nameof(Index), new VersionValidateViewModel() {IsCancellationEnabled = true});
		}
		
		[HttpGet]
		public async Task<IActionResult> LatestValidationResult()
		{
			var result = await _context.VersionValidateResult.OrderByDescending(s => s.ValidationDate).FirstOrDefaultAsync();
			return View(result);
		}

		[HttpGet]
		public IActionResult CancelValidationProcess()
		{
			if (!_validationCancellationService.IsCancellationAvailable)
			{
				return RedirectToAction(nameof(Index), new VersionValidateViewModel(){ Message = _resourcesWrapper.CancelingCancelledOperationMessage});
			}
			if(_validationCancellationService.IsCancellationRequested)
			{
				return RedirectToAction(nameof(Index), new VersionValidateViewModel(){ Message = _resourcesWrapper.CancellationOperationHasAlreadyStaredMessage});
			}
			if (_validationCancellationService.IsTokenDisposed)
			{
				return RedirectToAction(nameof(Index), new VersionValidateViewModel() { Message = _resourcesWrapper.CancellationTokenDisposedMessage});
			}
			_validationCancellationService.CancelValidation();
			return RedirectToAction(nameof(Index), new VersionValidateViewModel(){ Message = _resourcesWrapper.CancelingValidation, IsCancellationEnabled = false});
		}

		[HttpGet]
		public IActionResult ValidationResults(int page = 1, string dateOrder = null)
		{
			IEnumerable<VersionValidateResultModel> results;
			if (dateOrder is not null && dateOrder.Equals(Ascending))
			{
				results = _context.VersionValidateResult.OrderBy(s => s.ValidationDate).AsEnumerable();
				ViewData[nameof(dateOrder)] = dateOrder;
			}
			else
			{
				results = _context.VersionValidateResult.OrderByDescending(s => s.ValidationDate).AsEnumerable();
				ViewData[nameof(dateOrder)] = Descending;
			}
			var detailedPage = _pageFactory.CreateDetailedPage(results, page, PageSize);
			return View(detailedPage);
		}

		[HttpGet]
		public async Task<IActionResult> DeleteValidationData()
		{
			await _context.VersionValidateResult.RemoveAllData();
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index), new VersionValidateViewModel() { Message = string.Format(_resourcesWrapper.DataDeleteProcessStarted, "Validate Result Model")});
		}
	}
}
