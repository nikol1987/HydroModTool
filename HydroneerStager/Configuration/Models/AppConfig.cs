using System;
using System.Collections.Generic;

namespace HydroModTools.Configuration.Models
{
    internal sealed class AppConfig
    {
        public AppConfig(GeneralConfig generalConfig, GuidsConfig guidsConfig)
        {
            Projects = generalConfig.Projects;
            DefaultProject = generalConfig.DefaultProject;
            Guids = guidsConfig.Guids;
        }

        public List<ProjectConfig> Projects { get; }

        public Guid? DefaultProject { get; }

        public List<GuidConfigItem> Guids { get; }
    }
}
