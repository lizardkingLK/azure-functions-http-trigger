using AzureFunctionSample.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

using static AzureFunctionSample.Shared.SampleErrorResult;

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

        try
        {
            string name = req.Query.TryGetValue("name", out StringValues value)
            ? value.ToString()
            : "Anonymous";

            string optionsString = _customService.ConcatOptions();

            string response = string.Format(
                "Hello {0}!\nYourSample Configuration Options Are Below:\n{1}",
                "Anonymous",
                optionsString);

            return new OkObjectResult(response);
        }
        catch (Exception ex)
        {
            return GetError(ex.Message);
        }
    }
}