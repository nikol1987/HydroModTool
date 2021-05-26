using HydroModTools;
using HydroModTools.Common.Models;
using HydroModTools.Contracts.Services;
using HydroneerStager.Tools;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HydroneerStager.Services
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

        public async Task AddProject(Guid id, string name, string assetsPath, string outputPath)
        {
            await _configurationService.AddProject(id, name, assetsPath, outputPath);
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

            var gameFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Mining", "Saved", "Paks");
            Directory.CreateDirectory(gameFolder);

            var outFile = Utilities.GetOutFile(project);

            if (!File.Exists(outFile))
            {
                return;
            }

            var gamePak = Path.Combine(gameFolder, Path.GetFileName(outFile));

            if (File.Exists(gamePak))
            {
                File.Delete(gamePak);
            }

            reportProgress.Invoke(new ProgressbarStateModel((int)Math.Floor(Utilities.Remap(80, 0, 100, progressMin, progressMax)), "Copying pak"));

            File.Copy(outFile, gamePak);
        }

        public Task DeleteProject(Guid projectId)
        {
            throw new NotImplementedException();
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
