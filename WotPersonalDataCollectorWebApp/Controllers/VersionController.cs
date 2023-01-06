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
			// if (viewModel == null)
			// {
			// 	return View(new VersionValidateViewModel() {})
			// }
			return View(viewModel);
		}

		[HttpGet]
		public async Task<IActionResult> RequestValidationProcess(CancellationToken token)
		{
			_validationCancellationService.GetValidationCancellationToken(token);
			ThreadPool.QueueUserWorkItem(s => _validationService.RequestValidationProcess());
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
				return RedirectToAction(nameof(Index), new VersionValidateViewModel(){ Message = "Operation cancellation has already started"});
			}
			_validationCancellationService.CancelValidation();
			return RedirectToAction(nameof(Index));
		}
	}
}
