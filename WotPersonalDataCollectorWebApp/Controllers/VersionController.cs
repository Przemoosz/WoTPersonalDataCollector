namespace WotPersonalDataCollectorWebApp.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using CosmosDb.Context;
	using System.Threading.Tasks;
	using Models.ViewModels;
	using Services;
	using System.Threading;
	using Microsoft.EntityFrameworkCore;
	using Models;

	public sealed class VersionController: Controller, IVersionController
	{
		private const string Ascending = "Ascending";
		private const string Descending = "Descending";

		private readonly ICosmosDatabaseContext _context;
		private readonly IValidationCancellationService _validationCancellationService;
		private readonly IValidationService _validationService;
		

		public VersionController(ICosmosDatabaseContext context, IValidationCancellationService validationCancellationService, IValidationService validationService)
		{
			_context = context;
			_validationCancellationService = validationCancellationService;
			_validationService = validationService;
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
						Message = "Validation Operation has already started, can't start another one. Please wait."
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
				return RedirectToAction(nameof(Index), new VersionValidateViewModel(){ Message = "Can not cancel operation that was not started or is finished!"});
			}
			if(_validationCancellationService.IsCancellationRequested)
			{
				return RedirectToAction(nameof(Index), new VersionValidateViewModel(){ Message = "Operation cancellation has already started."});
			}
			if (_validationCancellationService.IsTokenDisposed)
			{
				return RedirectToAction(nameof(Index), new VersionValidateViewModel() { Message = "Cancellation token was disposed, that means validation operation is finished." });
			}
			_validationCancellationService.CancelValidation();
			return RedirectToAction(nameof(Index), new VersionValidateViewModel(){ Message = "Canceling validation", IsCancellationEnabled = false});
		}

		[HttpGet, Route("Results")]
		public IActionResult ValidationResults(int page = 1, string dateOrder = null)
		{
			IEnumerable<VersionValidateResultModel> results;
			if (dateOrder is not null && dateOrder.Equals(Ascending))
			{
				results = _context.VersionValidateResult.OrderBy(s => s.ValidationDate).AsEnumerable();
				ViewData["dateOrder"] = dateOrder;
			}
			else
			{
				results = _context.VersionValidateResult.OrderByDescending(s => s.ValidationDate).AsEnumerable();
				ViewData["dateOrder"] = Descending;
			}
			return View(results);
		}
	}
}
