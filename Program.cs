using AzureFunctionSample.Configurations.Options;
using AzureFunctionSample.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

internal class Program
{
    public static void Main()
    {
        IHost? host = new HostBuilder()
        .ConfigureFunctionsWebApplication()
        .ConfigureAppConfiguration(config =>
        {
            config.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);
        })
        .ConfigureServices((context, services) =>
        {
            services.Configure<SampleOptions>(context.Configuration.GetSection(nameof(SampleOptions)));

            services.AddHttpClient<SampleService>((serviceProvider, httpClient) =>
            {
                SampleOptions options = serviceProvider.GetRequiredService<IOptions<SampleOptions>>().Value;
                httpClient.BaseAddress = new Uri(options.BaseAPIAddress);
            });
        })
        .Build();

        host.Run();
    }
}