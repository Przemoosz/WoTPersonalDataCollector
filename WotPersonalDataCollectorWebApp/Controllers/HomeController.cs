using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WotPersonalDataCollectorWebApp.Models;
using WotPersonalDataCollectorWebApp.Services;

namespace WotPersonalDataCollectorWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IValidationCancellationService _validationCancellationService;

    public HomeController(ILogger<HomeController> logger, IValidationCancellationService validationCancellationService)
    {
	    _logger = logger;
	    _validationCancellationService = validationCancellationService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}