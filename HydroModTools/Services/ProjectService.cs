﻿using HydroModTools.Common.Models;
using HydroModTools.Contracts.Services;
using HydroModTools.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using static HydroModTools.Common.Constants;

namespace HydroModTools.Services
{
    internal class ProjectService : IProjectsService
    {
        private readonly IConfigurationService _configurationService;
        private readonly Packager _packager;
        private readonly Stager _stager;

        public ProjectService(IConfigurationService configurationService, Packager packager, Stager stager)
        {
            _configurationService = configurationService;
            _packager = packager;
            _stager = stager;
        }

        public async Task AddProject(Guid id, string name, short modIndex, string assetsPath, string outputPath)
        {
            await _configurationService.AddProject(id, name, modIndex, assetsPath, outputPath);
        }
        public async Task EditProject(Guid id, string name, short modIndex, string assetsPath, string outputPath)
        {
            await _configurationService.EditProject(id, name, modIndex, assetsPath, outputPath);
        }
        public async Task DeleteProject(Guid projectId)
        {
            await _configurationService.RemoveProject(projectId);
        }

        public async Task AddAssets(Guid projectId, IReadOnlyCollection<string> fileDirs)
        {
            await _configurationService.AddAssets(projectId, fileDirs);
        }
        public async Task RemoveAssets(Guid projectId, IReadOnlyCollection<Guid> assetsId)
        {
            await _configurationService.RemoveAssets(projectId, assetsId);
        }


        public async Task CopyProject(Guid id, int progressMin, int progressMax, Action<ProgressbarStateModel> reportProgress)
        {
            var configuration = await _configurationService.GetAsync();

            var project = configuration.Projects.FirstOrDefault(e => e.Id == id);

            if (project == null || project.Items == null || project.Items.Count == 0)
            {
                return;
            }

            reportProgress.Invoke(new ProgressbarStateModel((int)Math.Floor(Utilities.Remap(10, 0, 100, progressMin, progressMax)), "Checking Directory"));

            var outFile = Utilities.GetOutFile(project);

            if (!File.Exists(outFile))
            {
                return;
            }

            var gamePak = Path.Combine(PaksFolder, Path.GetFileName(outFile));

            try
            {
                if (File.Exists(gamePak))
                {
                    File.Delete(gamePak);
                }
            }
            catch (IOException)
            {

                MessageBox.Show("Check if the game is open or try to delete manualy.", "Can't copy mod");

                return;
            }

            reportProgress.Invoke(new ProgressbarStateModel((int)Math.Floor(Utilities.Remap(80, 0, 100, progressMin, progressMax)), "Copying pak"));

            try
            {
                File.Copy(outFile, gamePak);
            }
            catch (IOException)
            {
                MessageBox.Show("Check if the game is open or try to delete manualy.", "Can't copy mod");

                return;
            }
        }


        public async Task PackageProject(Guid id, int progressMin, int progressMax, Action<ProgressbarStateModel> reportProgress)
        {
            var configuration = await _configurationService.GetAsync();

            var project = configuration.Projects.FirstOrDefault(e => e.Id == id);

            if (project == null)
            {
                return;
            }

            reportProgress.Invoke(new ProgressbarStateModel((int)Math.Floor(Utilities.Remap(10, 0, 100, progressMin, progressMax)), "Start Packaging"));

            await _packager.PackageAsync((progress) =>
            {
                reportProgress.Invoke(progress);
            }, 10, 90, project);
        }

        public async Task StageProject(Guid id, int progressMin, int progressMax, Action<ProgressbarStateModel> reportProgress)
        {
            var configuration = await _configurationService.GetAsync();

            var project = configuration.Projects.FirstOrDefault(e => e.Id == id);

            if (project == null)
            {
                return;
            }

            reportProgress.Invoke(new ProgressbarStateModel((int)Math.Floor(Utilities.Remap(10, 0, 100, progressMin, progressMax)), "Start Staging"));

            await _stager.StageAsync((progress) =>
            {
                reportProgress.Invoke(progress);
            }, 10, 90, project, configuration.Guids);
        }
    }
}
