using AzureFunctionSample.Configurations.Options;
using AzureFunctionSample.Services;
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
            services.AddOptions<SampleOptions>()
                .Bind(context.Configuration.GetSection(nameof(SampleOptions)))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            services.AddHttpClient<SampleService>((serviceProvider, httpClient) =>
            {
                SampleOptions options = serviceProvider.GetRequiredService<IOptions<SampleOptions>>().Value;
                httpClient.BaseAddress = new Uri(options.BaseAPIAddress);
            });

            services.AddAuthorization();

            services.AddSingleton<TokenService>();
        })
        .Build();

        host.Run();
    }
}