namespace WotPersonalDataCollectorWebApp.Controllers
{
	using Exceptions;
	using Microsoft.AspNetCore.Mvc;
	using CosmosDb.Context;
	using CosmosDb.Dto;
	using CosmosDb.Dto.Version;
	using System.Threading.Tasks;
	using Models;
	using Models.ViewModels;
	using Services;

	public sealed class VersionController: Controller, IVersionController
	{
		private const string DtoType = "WotAccount";

		private readonly ICosmosDatabaseContext _context;
		private readonly IDtoVersionValidator _dtoVersionValidator;
		private readonly IValidationCancellationService _validationCancellationService;

		public VersionController(ICosmosDatabaseContext context, IDtoVersionValidator dtoVersionValidator, IValidationCancellationService validationCancellationService)
		{
			_context = context;
			_dtoVersionValidator = dtoVersionValidator;
			_validationCancellationService = validationCancellationService;
		}

		public IActionResult Index(VersionValidateViewModel viewModel = null)
		{
			return View(viewModel);
		}

		[HttpGet]
		public async Task<IActionResult> RequestValidationProcess(CancellationToken token)
		{
			CancellationToken cancellationToken = _validationCancellationService.GetValidationCancellationToken(token);
			var a = _validationCancellationService.GetValidationCancellationToken(token);
			Thread.Sleep(10000);
			var wotUserData = _context.PersonalData.AsAsyncEnumerable();
			var validationResult = await ValidateDto(wotUserData,cancellationToken);
			return RedirectToAction(nameof(Index));
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
				return RedirectToAction(nameof(Index), new VersionValidateViewModel(){ Message = "Operation cancellation is already started"});
			}
			_validationCancellationService.CancelValidation();
			return RedirectToAction(nameof(Index));
		}

		private async Task SaveValidationResult(VersionValidateResultModel validationResult)
		{
			
		}

		private async Task<VersionValidateResultModel> ValidateDto(IAsyncEnumerable<WotDataCosmosDbDto> wotData, CancellationToken cancellationToken)
		{
			int totalObjectsCount = 0;
			int wrongVersionCount = 0;
			int correctVersionCount = 0;
			int wrongObjectsCount = 0;
			await foreach (var data in wotData.WithCancellation(cancellationToken))
			{
				Thread.Sleep(4000);
				totalObjectsCount++;
				if (data.ClassProperties is null || !data.ClassProperties.Type.Equals(DtoType) || data.ClassProperties.DtoVersion is null)
				{
					wrongObjectsCount++;
					continue;
				}
				try
				{
					_dtoVersionValidator.EnsureVersionCorrectness(data);
					correctVersionCount++;
				}
				catch (DtoVersionComponentsException)
				{
					wrongObjectsCount++;
				}
				catch (DtoVersionException)
				{
					wrongVersionCount++;
				}
			}
			return new VersionValidateResultModel()
			{
				ValidationDate = DateTime.Now,
				CorrectVersionDtoCount = correctVersionCount,
				TotalItemsInCosmosDb = totalObjectsCount,
				WrongObjectsCount = wrongObjectsCount,
				WrongVersionDtoCount = wrongVersionCount,
				WasValidationCanceled = cancellationToken.IsCancellationRequested
			};
		}

	}
}
