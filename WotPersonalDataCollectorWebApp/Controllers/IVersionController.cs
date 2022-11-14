using Microsoft.AspNetCore.Mvc;

namespace WotPersonalDataCollectorWebApp.Controllers;

public interface IVersionController
{
	IActionResult Index();
	Task<IActionResult> RequestValidationProcess(CancellationToken token);
	Task<IActionResult> CancelValidationProcess();
}