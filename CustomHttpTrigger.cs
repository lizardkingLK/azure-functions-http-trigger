using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunctionSample;

public class CustomHttpTrigger(ILogger<CustomHttpTrigger> logger)
{
    private readonly ILogger<CustomHttpTrigger> _logger = logger;

    [Function("CustomHttpTrigger")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Custom Http Trigger!");
    }
}