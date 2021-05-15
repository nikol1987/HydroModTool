using Hydroneer.Contracts.Models;
using System;
using System.Threading.Tasks;

namespace Hydroneer.Contracts.WinFormsServices
{
    public interface IProjectsService
    {
        Task AddProject(Guid id, string name, string assetsPath, string outputPath);

        Task StageProject(Guid id, int progressMin, int progressMax, Action<ProgressbarStateModel> reportProgress);

        Task PackageProject(Guid id, int progressMin, int progressMax, Action<ProgressbarStateModel> reportProgress);

        Task CopyProject(Guid id, int progressMin, int progressMax, Action<ProgressbarStateModel> reportProgress);
    }
}
