using HydroModTools.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HydroModTools.Contracts.Services
{
    public interface IConfigurationService
    {
        Task SaveGuids(IReadOnlyCollection<GuidItemModel> guids);
        Task AddProject(Guid id, string name, string assetsPath, string outputPath);
        Task<AppConfigModel> GetAsync();
        Task Save();
    }
}
