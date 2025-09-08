using System.Text.Json;
using AzureFunctionSample.Configurations.Options;
using Microsoft.Extensions.Options;

namespace AzureFunctionSample.Services;

public class SampleService(HttpClient httpClient, IOptions<SampleOptions> options)
{
    private readonly HttpClient _httpClient = httpClient;

    private readonly IOptions<SampleOptions> _options = options;

    public string ConcatOptions()
    {
        return JsonSerializer.Serialize(_options.Value);
    }
}