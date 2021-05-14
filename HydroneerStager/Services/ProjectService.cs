using Hydroneer.Contracts.WinFormsServices;
using HydroneerStager.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HydroneerStager.Services
{
    public class ProjectService : IProjectsService
    {
        private readonly Configuration _configuration;

        public ProjectService(Configuration configuration)
        {
            _configuration = configuration;
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
    }
}
