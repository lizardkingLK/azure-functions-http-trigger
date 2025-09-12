using System.Text.Json;
using AzureFunctionSample.Configurations.Options;
using AzureFunctionSample.Shared;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AzureFunctionSample.Services;

public class SampleService(
    ILogger<SampleService> logger,
    IOptions<SampleOptions> options,
    HttpClient httpClient,
    TokenService tokenService)
{
    private readonly ILogger _logger = logger;

    private readonly HttpClient _httpClient = httpClient;

    private readonly TokenService _tokenService = tokenService;

    private readonly IOptions<SampleOptions> _options = options;

    public string ConcatOptions()
    {
        (TokenState? token, int cycle) = _tokenService.GetToken();
        if (token == null)
        {
            _logger.LogWarning("WARNING. tokens are in null/empty/pending state");
            token = _tokenService.CreateAccessToken();
        }

        return JsonSerializer.Serialize(new
        {
            Token = token,
            Cycle = cycle,
            Options = _options.Value
        });
    }
}