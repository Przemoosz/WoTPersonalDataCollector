namespace WotPersonalDataCollector.AzureMicroServicesFactory
{
	using System.IO;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Azure.WebJobs;
	using Microsoft.Azure.WebJobs.Extensions.Http;
	using Microsoft.AspNetCore.Http;
	using Microsoft.Extensions.Logging;
	using Newtonsoft.Json;
	using System.Diagnostics.CodeAnalysis;
	using System;
	using Utilities;
	using Authentication.Token;
	using Authorization;

	[ExcludeFromCodeCoverage]
	internal class AzureMicroServicesFactory
    {
	    private readonly ILogger<AzureMicroServicesFactory> _logger;
	    private readonly IMicroServicesConfiguration _configuration;
	    private readonly ITokenFactory _tokenFactory;
	    private readonly IAuthorizationService _authorizationService;

	    public AzureMicroServicesFactory(ILogger<AzureMicroServicesFactory> logger,
		    IMicroServicesConfiguration configuration, ITokenFactory tokenFactory,
		    IAuthorizationService authorizationService)
	    {
		    _logger = logger;
		    _configuration = configuration;
		    _tokenFactory = tokenFactory;
		    _authorizationService = authorizationService;
	    }

	    [FunctionName("AzureMicroServicesFactory")]
        public async Task<IActionResult> CreateCosmosDatabase(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "AzureMicroServicesFactory/CreateCosmosDatabase")] HttpRequest req)
        {
			//       _logger.LogInformation("C# HTTP trigger function processed a request.");
			// _logger.LogError(_tokenFactory.CreateBasicToken());
			// Console.WriteLine(_configuration.AdminUsername);
			//          string name = req.Query["name"];
			//
			//          string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
			//          dynamic data = JsonConvert.DeserializeObject(requestBody);
			//          name = name ?? data?.name;
			//
			//          string responseMessage = string.IsNullOrEmpty(name)
			//              ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
			//              : $"Hello, {name}. This HTTP triggered function executed successfully.";
			if (_authorizationService.IsAuthorized(req))
			{
				return new OkObjectResult("OK dude!");
			}
			return new UnauthorizedResult();
            //return new OkObjectResult(responseMessage);
        }
    }
}
