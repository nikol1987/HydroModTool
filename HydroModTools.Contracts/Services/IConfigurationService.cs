using HydroModTools.Contracts.Enums;
using HydroModTools.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HydroModTools.Contracts.Services
{
    public interface IConfigurationService
    {
        Task<AppConfigModel> GetAsync();
        Task Save();

        Task AddProject(Guid id, string name, short modIndex, string assetsPath, string outputPath);
        Task EditProject(Guid id, string name, short modIndex, string assetsPath, string outputPath);
        Task RemoveProject(Guid projectId);
        
        Task AddAssets(Guid projectId, IReadOnlyCollection<string> fileDirs);
        Task RemoveAssets(Guid projectId, IReadOnlyCollection<Guid> assetsId);
        
        Task SaveGuids(IReadOnlyCollection<GuidItemModel> guids);

        Task SetGameVersion(HydroneerVersion hydroneerVersion);
    }
}
