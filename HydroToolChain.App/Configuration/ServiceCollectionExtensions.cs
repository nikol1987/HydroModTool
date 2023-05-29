using HydroToolChain.App.Configuration.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HydroToolChain.App.Configuration;

public static class ServiceCollectionExtensions
{
    public static void ConfigureWritable<T>(  
        this IServiceCollection services,
        string file = "appsettings.json") where T : class, new()  
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile(file, true)
            .Build();
        
        services.AddScoped<IConfiguration>(_ => configuration);
        services.Configure<T>(configuration);
        
        services.AddTransient<IWritableOptions<T>>(provider =>  
        {  
            var options = provider.GetRequiredService<IOptionsMonitor<T>>();  
            return new WritableOptions<T>(options, configuration, file);  
        });
    }
}