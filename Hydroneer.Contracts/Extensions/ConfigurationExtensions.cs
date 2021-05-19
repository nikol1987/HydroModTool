using Hydroneer.Contracts.Models.AppModels;
using HydroneerStager.Contracts.Models.AppModels;
using HydroneerStager.Contracts.Models.WinformModels;
using System;
using System.Collections.Generic;

namespace HydroneerStager.Contracts.Extensions
{
    public static class ConfigurationExtensions
    {
        public static AppStateModel ToAppSateModel(this ConfigurationModel appConfig)
        {
            return new AppStateModel()
            {
                SelectedProject = appConfig.AppConfiguration.DefaultProject,
                Projects = appConfig.AppConfiguration.Projects.ToProjectsModel(),
                Guids = appConfig.GuidConfiguration.Guids.ToGuidsModel()
            };
        }

        public static AppConfiguration Update(this AppConfiguration appConfig, AppStateModel appState)
        {
            appConfig.DefaultProject = appState.SelectedProject;
            appConfig.Projects = appState.Projects.ToProjects();

            return appConfig;
        }

        public static List<Project> ToProjects(this IReadOnlyCollection<ProjectModel> projects)
        {
            var result = new List<Project>();

            if (projects == null)
            {
                return result;
            }

            foreach (var project in projects)
            {
                result.Add(new Project(project.Id, project.Name, project.Path, project.OutputPath, project.Items.ToProjectItem()));
            }

            return result;
        }

        public static IList<ProjectItem> ToProjectItem(this IReadOnlyCollection<ProjectItemModel> projectItems)
        {
            var result = new List<ProjectItem>();

            if (projectItems == null)
            {
                return result;
            }

            foreach (var item in projectItems)
            {
                result.Add(new ProjectItem(item.Id, item.Name, item.Path));
            }

            return result;
        }


        public static IReadOnlyCollection<ProjectModel> ToProjectsModel(this IList<Project> projects)
        {
            var result = new List<ProjectModel>();

            if (projects == null)
            {
                return result;
            }

            foreach (var project in projects)
            {
                result.Add(new ProjectModel(project.Id, project.Name, project.Path, project.OutputPath, project.Items.ToProjectItemsModel()));
            }

            return result;
        }

        public static IReadOnlyCollection<ProjectItemModel> ToProjectItemsModel(this IList<ProjectItem> projectItems)
        {
            var result = new List<ProjectItemModel>();

            if (projectItems == null)
            {
                return result;
            }

            foreach (var item in projectItems)
            {
                result.Add(new ProjectItemModel(item.Id, item.Name, item.Path));
            }

            return result;
        }

        public static IReadOnlyCollection<GuidModel> ToGuidsModel(this IReadOnlyCollection<GuidItem> guids)
        {
            var result = new List<GuidModel>();

            if (guids == null)
            {
                return result;
            }

            foreach (var guid in guids)
            {
                result.Add(new GuidModel(guid.Id, guid.Name, new Guid(guid.ModdedGuid), new Guid(guid.OriginalGuid)));
            }

            return result;
        }

        public static List<GuidItem> ToGuids(this IReadOnlyCollection<GuidModel> guids)
        {
            var result = new List<GuidItem>();

            if (guids == null)
            {
                return result;
            }

            foreach (var guid in guids)
            {
                result.Add(new GuidItem(guid.Id, guid.Name, guid.ModdedGuid.ToString("N"), guid.OriginalGuid.ToString("N")));
            }

            return result;
        }
    }
}
