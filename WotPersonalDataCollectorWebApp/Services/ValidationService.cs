using WotPersonalDataCollector.WebApp.CosmosDb.Context;
using WotPersonalDataCollector.WebApp.CosmosDb.Dto;
using WotPersonalDataCollector.WebApp.CosmosDb.Dto.Version;
using WotPersonalDataCollector.WebApp.Exceptions;
using WotPersonalDataCollector.WebApp.Models;

namespace WotPersonalDataCollector.WebApp.Services
{
	/// <inheritdoc />>
	internal sealed class ValidationService: IValidationService
	{
		private readonly IDtoVersionValidator _dtoVersionValidator;
		private readonly ICosmosContext _cosmosContext;
		private readonly IValidationCancellationService _validationCancellationService;
		private const string DtoType = "WotAccount";

		public bool IsValidationFinished { get; private set; } = true;

		public ValidationService(IDtoVersionValidator dtoVersionValidator, ICosmosContext cosmosContext, IValidationCancellationService validationCancellationService)
		{ 
			_dtoVersionValidator = dtoVersionValidator;
			_cosmosContext = cosmosContext;
			_validationCancellationService = validationCancellationService;
		}

		public async Task RunValidationProcessAsync()
		{
			IsValidationFinished = false;
			var wotUserData =  _cosmosContext.PersonalData.AsAsyncEnumerable();
			var validationResult = await ValidateDto(wotUserData);
			await SaveValidationResult(validationResult);
			IsValidationFinished = true;
			_validationCancellationService.Dispose();
		}
		
		private async Task SaveValidationResult(VersionValidateResultModel validationResult)
		{
			await _cosmosContext.VersionValidateResult.AddAsync(validationResult);
			await _cosmosContext.SaveChangesAsync();
		}

		private async Task<VersionValidateResultModel> ValidateDto(IAsyncEnumerable<WotDataCosmosDbDto> wotData)
		{
			int totalObjectsCount = 0;
			int wrongVersionCount = 0;
			int correctVersionCount = 0;
			int wrongObjectsCount = 0;
			await foreach (var data in wotData)
			{
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
				if (_validationCancellationService.IsCancellationRequested)
				{ 
					break;
				}
			}

			return new VersionValidateResultModel()
			{
				Id = Guid.NewGuid().ToString("D"),
				ValidationDate = DateTime.Now,
				CorrectVersionDtoCount = correctVersionCount,
				TotalItemsInCosmosDb = totalObjectsCount,
				WrongObjectsCount = wrongObjectsCount,
				WrongVersionDtoCount = wrongVersionCount,
				WasValidationCanceled = _validationCancellationService.IsCancellationRequested
			};
		}
	}
}
