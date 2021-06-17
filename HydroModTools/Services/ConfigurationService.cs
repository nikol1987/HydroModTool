﻿using HydroModTools.Contracts.Models;
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

        public async Task AddProject(Guid id, string name, string assetsPath, string outputPath)
        {
            var config = await _configuration.GetConfigurationAsync();

            var projects = config.Projects.ToModel().ToList();
            projects.Add(new ProjectModel(id, name, assetsPath, outputPath));

            var newConfig = new AppConfigModel(projects, config.DefaultProject, config.Guids.ToModel());

            await _configuration.SaveConfigurationAsync(newConfig);
        }
        public async Task RemoveProject(Guid projectId)
        {
            var config = await _configuration.GetConfigurationAsync();

            var projects = config.Projects.ToModel().Where(project => project.Id != projectId).ToList();

            var newConfig = new AppConfigModel(projects, config.DefaultProject, config.Guids.ToModel());

            await _configuration.SaveConfigurationAsync(newConfig);
        }

        public async Task AddAssets(Guid projectId, IReadOnlyCollection<string> fileDirs)
        {
            var config = await _configuration.GetConfigurationAsync();

            var project = config.Projects.First(project => project.Id == projectId).ToModel();
            var projectIdx = config.Projects.ToModel().ToList().IndexOf(project) - 1;
            projectIdx = projectIdx <= 0 ? 0 : projectIdx;

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

            var newProject = new ProjectModel(project.Id, project.Name, project.Path, project.OutputPath, items);

            var updatedProjects = config.Projects.Where(p => p.Id != projectId).ToList().ToModel().ToList();
            updatedProjects.Insert(projectIdx, newProject); 

            var newConfig = new AppConfigModel(updatedProjects, config.DefaultProject, config.Guids.ToModel());

            await _configuration.SaveConfigurationAsync(newConfig);
        }
        public async Task RemoveAssets(Guid projectId, IReadOnlyCollection<Guid> assetsId)
        {
            var config = await _configuration.GetConfigurationAsync();

            var project = config.Projects.First(project => project.Id == projectId).ToModel();
            var projectIdx = config.Projects.ToModel().ToList().IndexOf(project) - 1;
            projectIdx = projectIdx <= 0 ? 0 : projectIdx;

            var items = project.Items.Where(item => !assetsId.Contains(item.Id)).ToList();

            var newProject = new ProjectModel(project.Id, project.Name, project.Path, project.OutputPath, items);

            var updatedProjects = config.Projects.Where(p => p.Id != projectId).ToList().ToModel().ToList();
            updatedProjects.Insert(projectIdx, newProject);

            var newConfig = new AppConfigModel(updatedProjects, config.DefaultProject, config.Guids.ToModel());

            await _configuration.SaveConfigurationAsync(newConfig);
        }

        public async Task SaveGuids(IReadOnlyCollection<GuidItemModel> guids)
        {
            throw new NotImplementedException();
        }
    }
}
