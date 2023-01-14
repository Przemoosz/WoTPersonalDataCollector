using Microsoft.AspNetCore.Mvc;
using WotPersonalDataCollectorWebApp.Models.ViewModels;

namespace WotPersonalDataCollectorWebApp.Controllers;

public interface IVersionController
{
	IActionResult Index(VersionValidateViewModel viewModel = null);
	IActionResult RequestValidationProcess(CancellationToken token);
	IActionResult CancelValidationProcess();
	Task<IActionResult> LatestValidationResult();
	IActionResult ValidationResults(string dateOrder);
}