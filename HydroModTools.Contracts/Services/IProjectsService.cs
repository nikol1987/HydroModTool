﻿using HydroModTools.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HydroModTools.Contracts.Services
{
    public interface IProjectsService
    {
        Task AddProject(Guid id, string name, short modIndex, string assetsPath, string outputPath);
        Task EditProject(Guid id, string name, short modIndex, string assetsPath, string outputPath);
        Task DeleteProject(Guid projectId);

        Task AddAssets(Guid projectId, IReadOnlyCollection<string> filedirs);
        Task RemoveAssets(Guid projectId, IReadOnlyCollection<Guid> assetsIds);


        Task StageProject(Guid id, int progressMin, int progressMax, Action<ProgressbarStateModel> reportProgress);

        Task PackageProject(Guid id, int progressMin, int progressMax, Action<ProgressbarStateModel> reportProgress);

        Task CopyProject(Guid id, int progressMin, int progressMax, Action<ProgressbarStateModel> reportProgress);
    }
}
