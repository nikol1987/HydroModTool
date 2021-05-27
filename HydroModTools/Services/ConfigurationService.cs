using HydroModTools.Contracts.Models;
using HydroModTools.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HydroModTools.Services
{
    internal sealed class ConfigurationService : IConfigurationService
    {
        public async Task AddProject(Guid id, string name, string assetsPath, string outputPath)
        {
            throw new NotImplementedException();
        }

        public async Task<AppConfigModel> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }

        public async Task SaveGuids(IReadOnlyCollection<GuidItemModel> guids)
        {
            throw new NotImplementedException();
        }
    }
}
