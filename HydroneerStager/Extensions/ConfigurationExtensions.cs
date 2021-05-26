﻿using HydroModTools.Configuration.Models;
using HydroModTools.Contracts.Models;
using System;
using System.Collections.Generic;

namespace HydroModTools.Extensions
{
    internal static class ConfigurationExtensions
    {
        public static AppConfigModel ToModel(this AppConfig appConfig)
        {
            return new AppConfigModel(appConfig.Projects.ToModel(), appConfig.DefaultProject, appConfig.Guids.ToModel());
        }

        public static IReadOnlyCollection<ProjectModel> ToModel(this IList<ProjectConfig> projects)
        {
            var result = new List<ProjectModel>();

            if (projects == null)
            {
                return result;
            }

            foreach (var project in projects)
            {
                result.Add(new ProjectModel(project.Id, project.Name, project.Path, project.OutputPath, project.Items.ToModel()));
            }

            return result;
        }

        public static IReadOnlyCollection<ProjectItemModel> ToModel(this IList<ProjectItemConfig> projectItems)
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

        public static IReadOnlyCollection<GuidItemModel> ToModel(this IReadOnlyCollection<GuidConfigItem> guids)
        {
            var result = new List<GuidItemModel>();

            if (guids == null)
            {
                return result;
            }

            foreach (var guid in guids)
            {
                result.Add(new GuidItemModel(guid.Id, guid.Name, new Guid(guid.ModdedGuid), new Guid(guid.OriginalGuid)));
            }

            return result;
        }

        public static GeneralConfig ToGeneralConfig(this AppConfigModel appConfig)
        {
            return new GeneralConfig()
            {
                Projects = appConfig.Projects.ToConfig(),
                DefaultProject = appConfig.DefaultProject
            };
        }

        public static List<ProjectConfig> ToConfig(this IReadOnlyCollection<ProjectModel> projects)
        {
            var result = new List<ProjectConfig>();

            if (projects == null)
            {
                return result;
            }

            foreach (var project in projects)
            {
                result.Add(new ProjectConfig()
                {
                    Id = project.Id,
                    Name = project.Name,
                    Path = project.Path,
                    OutputPath = project.OutputPath,
                    Items = project.Items.ToConfig()
                });
            }

            return result;
        }

        public static List<ProjectItemConfig> ToConfig(this IReadOnlyCollection<ProjectItemModel> projectItems)
        {
            var result = new List<ProjectItemConfig>();

            if (projectItems == null)
            {
                return result;
            }

            foreach (var item in projectItems)
            {
                result.Add(new ProjectItemConfig()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Path = item.Path
                });
            }

            return result;
        }
    }
}
