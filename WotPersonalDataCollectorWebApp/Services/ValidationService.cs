using WotPersonalDataCollectorWebApp.CosmosDb.Context;
using WotPersonalDataCollectorWebApp.CosmosDb.Dto;
using WotPersonalDataCollectorWebApp.CosmosDb.Dto.Version;
using WotPersonalDataCollectorWebApp.Exceptions;
using WotPersonalDataCollectorWebApp.Models;

namespace WotPersonalDataCollectorWebApp.Services
{
	internal sealed class ValidationService: IValidationService
	{
		private readonly IDtoVersionValidator _dtoVersionValidator;
		private readonly ICosmosContext _cosmosContext;
		private readonly IValidationCancellationService _validationCancellationService;
		private const string DtoType = "WotAccount";

		public ValidationService(IDtoVersionValidator dtoVersionValidator, ICosmosContext cosmosContext, IValidationCancellationService validationCancellationService)
		{ 
			_dtoVersionValidator = dtoVersionValidator;
			_cosmosContext = cosmosContext;
			_validationCancellationService = validationCancellationService;
		}

		public async Task RequestValidationProcess()
		{
			var wotUserData =  _cosmosContext.PersonalData.AsAsyncEnumerable();
			var validationResult = await ValidateDto(wotUserData);
			await SaveValidationResult(validationResult);
		}

		private async Task SaveValidationResult(VersionValidateResultModel validationResult)
		{
			_cosmosContext.VersionValidateResult.Add(validationResult);
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
				Thread.Sleep(3000);
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
