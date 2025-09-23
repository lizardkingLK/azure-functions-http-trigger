using AzureFunctionSample.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunctionSample.Functions;

public class SampleTimerTrigger(ILogger<SampleTimerTrigger> logger, TokenService tokenService)
{
    private readonly ILogger<SampleTimerTrigger> _logger = logger;

    private readonly TokenService _tokenService = tokenService;

    [Function(nameof(SampleTimerTrigger))]
    public IActionResult Run(
        [TimerTrigger("0 */10 * * * *")] TimerInfo timerInfo, FunctionContext context)
    {
        _logger.LogInformation(
            "\tFunction Ran. Next timer schedule = {next}",
            timerInfo.ScheduleStatus?.Next);

        _tokenService.SetTokens();

        _logger.LogInformation("\t\tTokens Were Set.");

        return new OkObjectResult(new { context.FunctionId })
        {
            StatusCode = 200,
        };
    }
}