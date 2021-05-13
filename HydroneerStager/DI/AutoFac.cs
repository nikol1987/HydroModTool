using Autofac;
using System.Net.Http;

namespace HydroneerStager.DI
{
    internal static class AutoFac
    {
        public static IContainer Services;

        public static void BuildDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<AutofacFormFactory>().As<IFormFactory>();

            builder.Register(ct => {
                return new HttpClient();
            });

            RegisterDataAccess(builder);

            RegisterForms(builder);

            var container = builder.Build();

            FormFactory.Use(container.Resolve<IFormFactory>());

            Services = container;
        }

        private static void RegisterDataAccess(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(DataAccess.Assembly).Assembly)
            .Where(t => t.Name.EndsWith("Service"))
            .InstancePerLifetimeScope();
        }

        private static void RegisterForms(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(WinForms.Assembly).Assembly)
            .Where(t => t.Name.EndsWith("ViewModel"))
            .InstancePerDependency();

            builder.RegisterAssemblyTypes(typeof(WinForms.Assembly).Assembly)
            .Where(t => t.Name.EndsWith("View"))
            .SingleInstance();
        }
    }
}
