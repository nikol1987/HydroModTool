using Hydroneer.Contracts.Models.AppModels;
using Hydroneer.Contracts.WinFormsServices;
using HydroneerStager.Contracts.Models.AppModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HydroneerStager.Services
{
    public sealed class GuidsService : IGuidsService
    {
        private readonly Configuration _configuration;

        public GuidsService(Configuration configuration)
        {
            _configuration = configuration;
        }

        public async Task SaveGuids(IReadOnlyCollection<GuidItem> guids)
        {
            var appConfig = await _configuration.GetConfigurationAsync();

            await _configuration.Save(new ConfigurationModel(appConfig.AppConfiguration, new GuidConfiguration(guids)), Configuration.ConfigFile.Guids);
        }
    }
}
