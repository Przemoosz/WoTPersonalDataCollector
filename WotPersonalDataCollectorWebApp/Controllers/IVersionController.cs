using Microsoft.AspNetCore.Mvc;
using WotPersonalDataCollectorWebApp.Models.ViewModels;

namespace WotPersonalDataCollectorWebApp.Controllers;

public interface IVersionController
{
	IActionResult Index(VersionValidateViewModel viewModel = null);
	Task<IActionResult> RequestValidationProcess(CancellationToken token);
	Task<IActionResult> CancelValidationProcess();
}