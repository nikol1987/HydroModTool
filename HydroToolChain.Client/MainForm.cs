using HydroToolChain.App.Configuration;
using HydroToolChain.App.Configuration.Data;
using HydroToolChain.Blazor;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MudBlazor.Extensions;
using ServiceCollectionExtensions = HydroToolChain.Blazor.ServiceCollectionExtensions;

namespace HydroToolChain.Client;

public partial class MainForm : Form
{

    public MainForm()
    {
        InitializeComponent();
        
        var services = new ServiceCollection();
        ConfigureServices(services);
        var serviceProvider = services.BuildServiceProvider();
        
        blazorWebView1.RootComponents.Add<Blazor.Components.App>("#app");
        blazorWebView1.Services = serviceProvider;
        blazorWebView1.HostPage = "wwwroot\\index.html";
    }

    private void ConfigureServices(IServiceCollection services)
    {
        #region Configuration

        services.AddOptions();
        services.ConfigureWritable<AppData>("appData.json");

        #endregion

        #region Blazor

        services.AddWindowsFormsBlazorWebView();
        services.RegisterBlazorServices(options =>
        {
            options.FilesHelper = Helpers.ChooseFilesHelper;
        });

        #endregion

        #region Libs

        services.AddSingleton<HttpClient>();
        
        #endregion

#if DEBUG
        services.AddBlazorWebViewDeveloperTools();
#endif
    }
}