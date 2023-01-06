namespace WotPersonalDataCollectorWebApp.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using CosmosDb.Context;
	using System.Threading.Tasks;
	using Models;
	using Models.ViewModels;
	using Services;
	using System.Threading;
	using Microsoft.EntityFrameworkCore;

	public sealed class VersionController: Controller, IVersionController
	{
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
		public async Task<IActionResult> RequestValidationProcess(CancellationToken token)
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
		public async Task<IActionResult> ValidationResult(VersionValidateResultModel validationResult = null)
		{
			if (validationResult is null || validationResult.Id is null)
			{
				validationResult = await _context.VersionValidateResult.OrderByDescending(s => s.ValidationDate).FirstAsync();
			}
			return View(validationResult);
		}

		[HttpGet]
		public async Task<IActionResult> CancelValidationProcess()
		{
			if (!_validationCancellationService.IsCancellationAvailable)
			{
				return RedirectToAction(nameof(Index), new VersionValidateViewModel(){ Message = "Can not cancel operation that was not started!"});
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
	}
}
