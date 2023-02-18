using Microsoft.AspNetCore.Mvc;
using WotPersonalDataCollector.WebApp.Models.ViewModels;

namespace WotPersonalDataCollector.WebApp.Controllers;

public interface IVersionController
{
	IActionResult Index(VersionValidateViewModel viewModel = null);
	IActionResult RequestValidationProcess(CancellationToken token);
	IActionResult CancelValidationProcess();
	Task<IActionResult> LatestValidationResult();
	IActionResult ValidationResults(int page, string dateOrder);
	Task<IActionResult> DeleteValidationData();
}