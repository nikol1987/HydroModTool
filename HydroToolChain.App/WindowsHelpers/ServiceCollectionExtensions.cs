using Microsoft.Extensions.DependencyInjection;

namespace HydroToolChain.App.WindowsHelpers;

public static class ServiceCollectionExtensions
{
    public static void ConfigureWindowsHelpers (  
        this IServiceCollection services)
    {
        services.AddTransient<IWindowsHelpers, WindowsHelpers>();
    }
}