using HydroModTools.Configuration.Enums;
using System;
using System.Collections.Generic;

namespace HydroModTools.Configuration.Models
{
    internal sealed class AppConfig
    {
        public AppConfig(GeneralConfig generalConfig, GuidsConfig guidsConfig)
        {
            Projects = generalConfig.Projects;
            Guids = guidsConfig.Guids;

            DefaultProject = generalConfig.DefaultProject;
            HydroneerVersion = generalConfig.HydroneerVersion;
        }

        public HydroneerVersion HydroneerVersion { get; }

        public List<ProjectConfig> Projects { get; }

        public Guid? DefaultProject { get; }

        public List<GuidConfigItem> Guids { get; }
    }
}
