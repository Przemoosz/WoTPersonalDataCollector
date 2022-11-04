using WotPersonalDataCollectorWebApp.Services;

namespace WotPersonalDataCollectorWebApp.Controllers
{
	using Exceptions;
	using Microsoft.AspNetCore.Mvc;
	using CosmosDb.Context;
	using CosmosDb.Dto;
	using CosmosDb.Dto.Version;

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

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Validate(CancellationToken token)
		{
			
			CancellationToken cancellationToken = _validationCancellationService.GetValidationCancellationToken(token);
			
			//Thread.Sleep(10000);
			// cts.Cancel();
			var a = _validationCancellationService.GetValidationCancellationToken(token);
			Thread.Sleep(10000);
			//_validationCancellationToken
			var wotUserData = _context.PersonalData.AsAsyncEnumerable();
			var validationResult = await ValidateDto(wotUserData,cancellationToken);
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public Task Cancel()
		{
			_validationCancellationService.CancelValidation();
			return Task.CompletedTask;
		}

		private async Task<VersionValidateModel> ValidateDto(IAsyncEnumerable<WotDataCosmosDbDto> wotData, CancellationToken cancellationToken)
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
			return new VersionValidateModel()
			{
				CorrectVersionDtoCount = correctVersionCount,
				TotalItemsInCosmosDb = totalObjectsCount,
				WrongObjectsCount = wrongObjectsCount,
				WrongVersionDtoCount = wrongVersionCount
			};
		}

	}

	public sealed class VersionValidateModel
	{
		public int TotalItemsInCosmosDb { get; init; }
		public int CorrectVersionDtoCount { get; init; }
		public int WrongObjectsCount { get; init; }
		public int WrongVersionDtoCount { get; init; }

	}

	public interface IVersionController
	{
	}
}
