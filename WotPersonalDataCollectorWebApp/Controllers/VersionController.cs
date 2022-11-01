namespace WotPersonalDataCollectorWebApp.Controllers
{
	using Exceptions;
	using Microsoft.AspNetCore.Mvc;
	using CosmosDb.Context;
	using CosmosDb.Dto;
	using CosmosDb.Dto.Version;
	/// <summary>
	/// Not implemented in this Version
	/// </summary>
	public sealed class VersionController: IVersionController
	{
		private const string DtoType = "WotAccount";

		private readonly ICosmosDatabaseContext _context;
		private readonly IDtoVersionValidator _dtoVersionValidator;

		public VersionController(ICosmosDatabaseContext context, IDtoVersionValidator dtoVersionValidator)
		{
			_context = context;
			_dtoVersionValidator = dtoVersionValidator;
		}

		// public IActionResult Index()
		// {
		// 	return View();
		// }

		// [HttpGet]
		// public async Task<IActionResult> Validate()
		// {
		// 	var wotUserData = _context.PersonalData.AsAsyncEnumerable();
		// 	var validationResult = await ValidateDto(wotUserData);
		// 	return RedirectToAction(nameof(Index));
		// }

		private async Task<VersionValidateModel> ValidateDto(IAsyncEnumerable<WotDataCosmosDbDto> wotData)
		{
			int totalObjectsCount = 0;
			int wrongVersionCount = 0;
			int correctVersionCount = 0;
			int wrongObjectsCount = 0;
			await foreach (var data in wotData)
			{
				totalObjectsCount++;
				if (data.ClassProperties == null || !data.ClassProperties.Type.Equals(DtoType) || data.ClassProperties.DtoVersion == null)
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
