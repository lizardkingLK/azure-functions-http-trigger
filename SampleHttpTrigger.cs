using AzureFunctionSample.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace AzureFunctionSample;

public class SampleHttpTrigger(ILogger<SampleHttpTrigger> logger, SampleService customService)
{
    private readonly ILogger<SampleHttpTrigger> _logger = logger;

    private readonly SampleService _customService = customService;

    [Function("SampleHttpTrigger")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous,
    "get",
    "post")] HttpRequest req)
    {
        _logger.LogInformation("SampleHttp trigger");

        return new OkObjectResult(
            string.Format("""
            Hello {0}!
            Your sample configuration options are below:
            {1}
            """,
                req.Query.TryGetValue(
                    "name",
                    out StringValues value)
                    ? value
                    : "Annonymous",
                    _customService.ConcatOptions()));
    }
}