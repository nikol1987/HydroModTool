using Autofac;
using Splat;

namespace HydroneerStager.WinForms.Converters
{
    public static class Converters
    {
        public static ContainerBuilder RegisterConverters(this ContainerBuilder builder)
        {
            Locator.CurrentMutable.RegisterConstant(new ProjectsConverter());

            builder.RegisterAssemblyTypes(typeof(Assembly).Assembly)
            .Where(t => t.Name.EndsWith("Converter"))
            .InstancePerDependency();

            return builder;
        }
    }
}
