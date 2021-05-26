using HydroModTools.Common;
using HydroModTools.Contracts.Services;
using HydroModTools.DI;
using HydroModTools.WinForms.Views;
using HydroneerStager.Tools;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HydroModTools
{
    internal sealed class HydroModTools : ApplicationContext
    {
        private Configuration.Configuration config = new Configuration.Configuration();

        public static IServiceProvider Services;

        public ApplicationView? MainForm { get; private set; }

        public void StartApplication()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices(async (context, services) => {
                    await ConfigureServices(null, services, ConfigureServicesPhase.PreConfig);
                })
                .ConfigureAppConfiguration((context, builder) => {
                    builder.AddJsonFile($"{AppVars.ConfigPath}.json", false, true);
                    builder.AddJsonFile($"{AppVars.GuidsConfigPath}.json", false, true);
                })
                .ConfigureServices(async (context, services) => {
                    await ConfigureServices(context.Configuration, services, ConfigureServicesPhase.PostConfig);
                })
                .Build();

            Services = host.Services;


            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var configuration = Services.GetService<IConfigurationService>();

            Application.ApplicationExit += async (sender, e) =>
            {

                await configuration.Save();
            };

            MainForm = FormFactory.Create<ApplicationView>();
            MainForm.Hide();

            var spashForm = FormFactory.Create<SpashView>();
            spashForm.TimeOutEvent += () =>
            {
                MainForm.Show();
            };

            spashForm.ShowDialog();

            Application.Run(this); // TODO: Anti-crash
        }

        private async Task ConfigureServices(IConfiguration appConfiguration, IServiceCollection services, ConfigureServicesPhase configureServicesPhase)
        {
            if (configureServicesPhase == ConfigureServicesPhase.PreConfig)
            {
                await config.SetupConfigurationAsync();

                return;
            }

            if (configureServicesPhase == ConfigureServicesPhase.PostConfig)
            {
                await config.LoadConfigAsync(appConfiguration);

                var formFactory = new DIFormFactory(Services);

                FormFactory.Use(formFactory);

                services
                    .AddSingleton<Packager>()
                    .AddSingleton<Stager>()
                    .AddSingleton<HttpClient>((_) => new HttpClient())
                    .Scan(s => {
                        s.FromAssemblyOf<Assembly>()
                         .FromAssemblyOf<DataAccess.Assembly>()
                         .AddClasses((filter) =>
                         {
                             filter.Where(type => type.Name.EndsWith("Service"));
                         })
                         .AsImplementedInterfaces()
                         .WithSingletonLifetime();

                        s.FromAssemblyOf<WinForms.Assembly>()
                         .AddClasses((filter) =>
                         {
                             filter.Where(type => type.Name.EndsWith("View"));
                         })
                         .AsSelf()
                         .WithSingletonLifetime();
                    });

                return;
            }
        }

        private enum ConfigureServicesPhase
        {
            PreConfig,
            PostConfig
        }
    }
}
