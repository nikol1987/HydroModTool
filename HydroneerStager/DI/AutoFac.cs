using Autofac;
using HydroneerStager.Extensions;
using HydroneerStager.WinForms.Converters;
using HydroneerStager.WinForms.Data;
using System.Net.Http;
using System.Threading.Tasks;

namespace HydroneerStager.DI
{
    internal static class AutoFac
    {
        public static IContainer Services;

        public static void BuildDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<AutofacFormFactory>().As<IFormFactory>();

            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
            .Where(t => t.Name.EndsWith("Service"))
            .InstancePerLifetimeScope()
            .AsImplementedInterfaces();

            builder.Register(ct => {
                return new HttpClient();
            });

            builder
                .RegisterDataAccess()
                .RegisterForms()
                .RegisterConverters();

            builder.RegisterType<Configuration>()
                .SingleInstance();

            var container = builder.Build();

            FormFactory.Use(container.Resolve<IFormFactory>());

            Services = container;
        }

        private static ContainerBuilder RegisterDataAccess(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(DataAccess.Assembly).Assembly)
            .Where(t => t.Name.EndsWith("Service"))
            .InstancePerLifetimeScope();

            return builder;
        }

        private static ContainerBuilder RegisterForms(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(WinForms.Assembly).Assembly)
            .Where(t => t.Name.EndsWith("Service"))
            .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(WinForms.Assembly).Assembly)
            .Where(t => t.Name.EndsWith("Model"))
            .InstancePerDependency();

            builder.RegisterAssemblyTypes(typeof(WinForms.Assembly).Assembly)
            .Where(t => t.Name.EndsWith("View"))
            .SingleInstance();

            builder.Register(ct => {
                var configuration = Services.Resolve<Configuration>();
                var appConfig = configuration.GetConfigurationAsync().Result;
                var appModel = appConfig.ToAppSateModel();

                var applicationStore = new ApplicationStore(appModel);
                applicationStore.LoadConfig += async () => await ApplicationStore_LoadConfig();
                applicationStore.SaveConfig += ApplicationStore_SaveConfig;
                
                return applicationStore;
            })
            .SingleInstance();

            return builder;
        }

        private static async void ApplicationStore_SaveConfig(AppStateModel appState)
        {
            var configuration = Services.Resolve<Configuration>();
            var updatedConfig = (await configuration.GetConfigurationAsync()).Update(appState);
            configuration.Save(updatedConfig);
        }

        private static async Task<AppStateModel> ApplicationStore_LoadConfig()
        {
            var configuration = Services.Resolve<Configuration>();

            var appConfig = await configuration.GetConfigurationAsync();

            return appConfig.ToAppSateModel();
        }
    }
}
