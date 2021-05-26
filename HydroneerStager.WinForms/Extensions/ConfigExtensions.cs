using HydroModTools.Contracts.Models;
using HydroModTools.WinForms.Data;
using System.Collections.Generic;

namespace HydroModTools.WinForms.Extensions
{
    internal static class ConfigExtensions
    {
        public static ApplicationStore ToStore(this AppConfigModel configModel)
        {
            return new ApplicationStore()
            {
                DefaultProject = configModel.DefaultProject,
                Projects = configModel.Projects.ToStore(),
                Guids = configModel.Guids.ToStore()
            };
        }

        public static IReadOnlyCollection<ProjectStore> ToStore(this IReadOnlyCollection<ProjectModel> projects)
        {
            var result = new List<ProjectStore>();

            if (projects == null)
            {
                return result;
            }

            foreach (var project in projects)
            {
                result.Add(new ProjectStore()
                {
                    Id = project.Id,
                    Name = project.Name,
                    Path = project.Path,
                    OutputPath = project.OutputPath,
                    Items = project.Items.ToStore()
                });
            }

            return result;
        }

        public static List<ProjectItemStore> ToStore(this IReadOnlyCollection<ProjectItemModel> projectItems)
        {
            var result = new List<ProjectItemStore>();

            if (projectItems == null)
            {
                return result;
            }

            foreach (var item in projectItems)
            {
                result.Add(new ProjectItemStore(item.Id, item.Name, item.Path));
            }

            return result;
        }

        public static List<GuidItemStore> ToStore(this IReadOnlyCollection<GuidItemModel> guids)
        {
            var result = new List<GuidItemStore>();

            if (guids == null)
            {
                return result;
            }

            foreach (var item in guids)
            {
                result.Add(new GuidItemStore()
                {
                    Id = item.Id,
                    Name = item.Name,
                    OriginalGuid = item.OriginalGuid,
                    ModdedGuid = item.ModdedGuid
                });
            }

            return result;
        }
    }
}
