using HydroneerStager.Contracts.Models.AppModels;

namespace Hydroneer.Contracts.Models.AppModels
{
    public sealed class ConfigurationModel
    {
        public ConfigurationModel(AppConfiguration appConfiguration, GuidConfiguration guidConfiguration)
        {
            AppConfiguration = appConfiguration;
            GuidConfiguration = guidConfiguration;
        }

        public AppConfiguration AppConfiguration { get; }

        public GuidConfiguration GuidConfiguration { get; }
    }
}
