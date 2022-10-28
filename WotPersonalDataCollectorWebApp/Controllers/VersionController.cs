﻿using WotPersonalDataCollectorWebApp.CosmosDb.Context;
using WotPersonalDataCollectorWebApp.CosmosDb.Dto;
using WotPersonalDataCollectorWebApp.CosmosDb.Dto.Metrics;
using WotPersonalDataCollectorWebApp.CosmosDb.Dto.Version;

namespace WotPersonalDataCollectorWebApp.Controllers
{
	using global::WotPersonalDataCollectorWebApp.Exceptions;
	using Microsoft.AspNetCore.Mvc;
	public sealed class VersionController : Controller, IVersionController
	{
		private readonly ICosmosDatabaseContext _context;
		private readonly IDtoVersionValidator _dtoVersionValidator;

		public VersionController(ICosmosDatabaseContext context, IDtoVersionValidator dtoVersionValidator)
		{
			_context = context;
			_dtoVersionValidator = dtoVersionValidator;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Validate()
		{
			var wotUserData = _context.PersonalData.AsAsyncEnumerable();
			var validationResult = await ValidateDto(wotUserData);
			return RedirectToAction(nameof(Index));
		}

		private async Task<VersionValidateModel> ValidateDto(IAsyncEnumerable<WotDataCosmosDbDto> wotData)
		{
			int totalObjectsCount = 0;
			int wrongVersionCount = 0;
			int correctVersionCount = 0;
			int wrongObjectsCount = 0;
			await foreach (var data in wotData)
			{
				totalObjectsCount++;
				if (data.ClassProperties == null || !data.ClassProperties.Type.Equals("WotAccount") || data.ClassProperties.DtoVersion == null)
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
