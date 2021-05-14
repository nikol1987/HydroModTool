using HydroneerStager.Models;
using HydroneerStager.WinForms.Data;
using System.Collections.Generic;

namespace HydroneerStager.Extensions
{
    internal static class ConfigurationExtensions
    {
        public static AppStateModel ToAppSateModel(this AppConfiguration appConfig)
        {
            return new AppStateModel()
            {
                SelectedProject = appConfig.DefaultProject,
                Projects = appConfig.Projects.ToProjectsModel()
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
    }
}
