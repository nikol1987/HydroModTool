using Hydroneer.Contracts.Models;
using Hydroneer.Contracts.WinFormsServices;
using HydroneerStager.Models;
using HydroneerStager.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HydroneerStager.Services
{
    public class ProjectService : IProjectsService
    {
        private readonly Configuration _configuration;
        private readonly Packager _packager;
        private readonly Stager _stager;

        public ProjectService(Configuration configuration, Packager packager, Stager stager)
        {
            _configuration = configuration;
            _packager = packager;
            _stager = stager;
        }

        public async Task AddProject(Guid id, string name, string assetsPath, string outputPath)
        {
            var configuration = await _configuration.GetConfigurationAsync();


            var newProjects = new List<Project>();
            newProjects.AddRange(configuration.Projects);
            newProjects.Add(new Project(id, name, assetsPath, outputPath, new List<ProjectItem>()));

            configuration.Projects = newProjects;
            _configuration.Save(configuration);
        }

        public async Task CopyProject(Guid id, int progressMin, int progressMax, Action<ProgressbarStateModel> reportProgress)
        {
            var configuration = await _configuration.GetConfigurationAsync();

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

        public async Task PackageProject(Guid id, int progressMin, int progressMax, Action<ProgressbarStateModel> reportProgress)
        {
            var configuration = await _configuration.GetConfigurationAsync();

            var project = configuration.Projects.FirstOrDefault(e => e.Id == id);

            if (project == null)
            {
                return;
            }

            reportProgress.Invoke(new ProgressbarStateModel((int)Math.Floor(Utilities.Remap(10, 0, 100, progressMin, progressMax)), "Start Packaging"));

            _packager.Package((progress) => {
                reportProgress.Invoke(progress);
            }, 10, 90, project);
        }

        public async Task StageProject(Guid id, int progressMin, int progressMax, Action<ProgressbarStateModel> reportProgress)
        {
            var configuration = await _configuration.GetConfigurationAsync();

            var project = configuration.Projects.FirstOrDefault(e => e.Id == id);

            if (project == null)
            {
                return;
            }

            reportProgress.Invoke(new ProgressbarStateModel((int)Math.Floor(Utilities.Remap(10, 0, 100, progressMin, progressMax)), "Start Staging"));

            _stager.Stage((progress) => {
                reportProgress.Invoke(progress);
            }, 10, 90, project, configuration.Guids);
        }
    }
}
