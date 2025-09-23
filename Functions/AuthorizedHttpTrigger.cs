using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunctionSample.Functions;

public class AuthorizedHttpTrigger(ILogger<AuthorizedHttpTrigger> logger)
{
    private readonly ILogger<AuthorizedHttpTrigger> _logger = logger;

    [Function(nameof(AuthorizedHttpTrigger))]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Admin, "get")] HttpRequest _)
    {
        _logger.LogInformation(nameof(AuthorizedHttpTrigger));

        try
        {
            return new OkObjectResult("Authorized") { StatusCode = StatusCodes.Status200OK };
        }
        catch (Exception)
        {
            return new ObjectResult("Not Authorized") { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}