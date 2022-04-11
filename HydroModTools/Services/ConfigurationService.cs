using HydroModTools.Contracts.Enums;
using HydroModTools.Contracts.Models;
using HydroModTools.Contracts.Services;
using HydroModTools.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public async Task<AppConfigModel> GetAsync()
        {
            var config = await _configuration.GetConfigurationAsync();

            return config.ToModel();
        }

        public async Task Save()
        {
            await _configuration.SaveConfigurationAsync();
        }

        public async Task AddProject(Guid id, string name, short modIndex, string assetsPath, string outputPath)
        {
            var config = await _configuration.GetConfigurationAsync();

            var projects = config.Projects.ToModel().ToList();
            projects.Add(new ProjectModel(id, name, modIndex, assetsPath, outputPath));

            var newConfig = new AppConfigModel(projects, config.DefaultProject, (HydroneerVersion)config.HydroneerVersion, config.Guids.ToModel());

            await _configuration.SaveConfigurationAsync(newConfig);
        }

        public async Task EditProject(Guid id, string name, short modIndex, string assetsPath, string outputPath)
        {
            var config = await _configuration.GetConfigurationAsync();

            var projects = config.Projects.ToModel().ToList();
            var projectIdx = projects.FindIndex(p => p.Id == id);

            projects[projectIdx] = new ProjectModel(id, name, modIndex, assetsPath, outputPath, projects[projectIdx].Items);

            var newConfig = new AppConfigModel(projects, config.DefaultProject, (HydroneerVersion)config.HydroneerVersion, config.Guids.ToModel());

            await _configuration.SaveConfigurationAsync(newConfig);
        }

        public async Task RemoveProject(Guid projectId)
        {
            var config = await _configuration.GetConfigurationAsync();

            var projects = config.Projects.ToModel().Where(project => project.Id != projectId).ToList();

            var newConfig = new AppConfigModel(projects, config.DefaultProject, (HydroneerVersion)config.HydroneerVersion, config.Guids.ToModel());

            await _configuration.SaveConfigurationAsync(newConfig);
        }

        public async Task AddAssets(Guid projectId, IReadOnlyCollection<string> fileDirs)
        {
            var config = await _configuration.GetConfigurationAsync();

            var projects = config.Projects.ToModel().ToList();
            var projectIdx = projects.FindIndex(p => p.Id == projectId);

            var project = projects[projectIdx];

            var items = project.Items.ToList();

            foreach (var item in fileDirs)
            {
                var partialPath = item.Replace(project.Path, "");

                var contains = project.Items.Any(i => i.Name == Path.GetFileName(item) && i.Path == partialPath);

                if (contains)
                {
                    continue;
                }

                items.Add(new ProjectItemModel(Guid.NewGuid(), Path.GetFileName(item), partialPath));
            }

            var newProject = new ProjectModel(project.Id, project.Name, project.ModIndex, project.Path, project.OutputPath, items);

            projects[projectIdx] = newProject;

            var newConfig = new AppConfigModel(projects, config.DefaultProject, (HydroneerVersion)config.HydroneerVersion, config.Guids.ToModel());

            await _configuration.SaveConfigurationAsync(newConfig);
        }

        public async Task RemoveAssets(Guid projectId, IReadOnlyCollection<Guid> assetsId)
        {
            var config = await _configuration.GetConfigurationAsync();

            var projects = config.Projects.ToModel().ToList();
            var projectIdx = projects.FindIndex(p => p.Id == projectId);

            var project = projects[projectIdx];

            var items = project.Items.Where(item => !assetsId.Contains(item.Id)).ToList();

            var newProject = new ProjectModel(project.Id, project.Name, project.ModIndex, project.Path, project.OutputPath, items);

            projects[projectIdx] = newProject;

            var newConfig = new AppConfigModel(projects, config.DefaultProject, (HydroneerVersion)config.HydroneerVersion, config.Guids.ToModel());

            await _configuration.SaveConfigurationAsync(newConfig);
        }

        public async Task SaveGuids(IReadOnlyCollection<GuidItemModel> guids)
        {
            var config = await _configuration.GetConfigurationAsync();

            var newConfig = new AppConfigModel(config.Projects.ToModel(), config.DefaultProject, (HydroneerVersion)config.HydroneerVersion, guids);

            await _configuration.SaveConfigurationAsync(newConfig);

        }

        public async Task SetGameVersion(HydroneerVersion hydroneerVersion)
        {
            var config = await _configuration.GetConfigurationAsync();

            var newConfig = new AppConfigModel(config.Projects.ToModel(), config.DefaultProject, hydroneerVersion, config.Guids.ToModel());

            await _configuration.SaveConfigurationAsync(newConfig);
        }
    }
}
