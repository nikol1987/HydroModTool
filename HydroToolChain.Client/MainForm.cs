using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using HydroToolChain.App.Configuration;
using HydroToolChain.App.Configuration.Data;
using HydroToolChain.App.Tools;
using HydroToolChain.Client.Business;
using HydroToolChain.Client.Business.Abstracts;
using MatBlazor;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using AppContext = System.AppContext;

namespace HydroToolChain.Client;

public partial class MainForm : Form
{
    private static string AboutMeUrl => "https://raw.githubusercontent.com/ResaloliPT/HydroModTool/master/Readme.md";
    public static string? AboutReadMe { get; private set; }
    
    public MainForm()
    {
        InitializeComponent();

        GetAboutReadme();
        
        var services = new ServiceCollection();

        ConfigureServices(services);
    }

    private void ConfigureServices(IServiceCollection services)
    {
        #region Configuration

        services.AddOptions();
        services.ConfigureWritable<AppData>("appData.json");

        #endregion

        #region Tools

        services.ConfigureTools<IMatDialogService>((options, dialog) =>
        {
            options.ShowMessage = (message, type) =>
            {
                switch (type)
                {
                    case MessageType.Error:
                    case MessageType.Warning:
                    case MessageType.Info:
                        dialog.AlertAsync(message);
                        break;
                }
            };
        });

        #endregion

        #region Business

        services.AddTransient(typeof(IAppFacade), typeof(AppFacade));
        services.AddSingleton<Business.AppContext>();

        #endregion

        #region Blazor

        services.AddWindowsFormsBlazorWebView();
        services.AddMatBlazor();
        services.AddMudServices();

        #endregion

        #region Libs

        services.AddSingleton<HttpClient>();
        services.AddMatToaster(config =>
        {
            config.Position = MatToastPosition.BottomRight;
            config.PreventDuplicates = false;
            config.NewestOnTop = true;
            config.VisibleStateDuration = 3000;
            config.MaximumOpacity = 75;
        });
        services.AddBlazorContextMenu();

        #endregion

#if DEBUG
        services.AddBlazorWebViewDeveloperTools();
#endif
        blazorWebView1.HostPage = "wwwroot\\index.html";
        blazorWebView1.Services = services.BuildServiceProvider();
        blazorWebView1.RootComponents.Add<Client.Components.App>("#app");
    }

    private void GetAboutReadme()
    {
        if (AboutReadMe != null)
        {
            return;
        }

        try
        {
            new HttpClient().GetStreamAsync(AboutMeUrl).ContinueWith(responseTask =>
            {
                using var stringStream = new StreamReader(responseTask.Result, Encoding.UTF8);

                AboutReadMe = stringStream.ReadToEnd();
            });
        }
        catch (Exception)
        {
            try
            {
                var currentDirectory = AppContext.BaseDirectory;
                var aboutFile = Path.Combine(currentDirectory, "About.md");

                AboutReadMe = File.ReadAllText(aboutFile);
            }
            catch (Exception)
            {
                AboutReadMe =
                    @"# Hydroneer Modding Toolchain [![Donate](https://www.paypalobjects.com/en_US/i/btn/btn_donate_SM.gif)](https://paypal.me/ResaloliPT)

                Check the application repository [Here](https://github.com/ResaloliPT/HydroModTool)";
            }
        }
    }
}