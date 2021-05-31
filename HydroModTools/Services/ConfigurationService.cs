using HydroModTools.Contracts.Models;
using HydroModTools.Contracts.Services;
using HydroModTools.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HydroModTools.Services
{
    internal sealed class ConfigurationService : IConfigurationService
    {
        private readonly Configuration.Configuration _configuration;

        public ConfigurationService(Configuration.Configuration configuration)
        {
            _configuration = configuration;
        }

        public async Task AddProject(Guid id, string name, string assetsPath, string outputPath)
        {
            throw new NotImplementedException();
        }

        public async Task<AppConfigModel> GetAsync()
        {
            var config = await _configuration.GetConfigurationAsync();

            return config.ToModel();
        }

        public async Task Save()
        {
            await _configuration.SaveConfigurationAsync();
        }

        public async Task SaveGuids(IReadOnlyCollection<GuidItemModel> guids)
        {
            throw new NotImplementedException();
        }
    }
}
