using HydroModTools.Configuration.Enums;
using System;
using System.Collections.Generic;
using HydroModTools.Contracts.Models;

namespace HydroModTools.Configuration.Models
{
    internal sealed class AppConfig
    {
        public AppConfig(GeneralConfig generalConfig, GuidsConfig guidsConfig, UIDsConfig uidsConfig)
        {
            Projects = generalConfig.Projects;
            Guids = guidsConfig.Guids;
            UIDs = uidsConfig.UIDs;
            
            DefaultProject = generalConfig.DefaultProject;
            HydroneerVersion = generalConfig.HydroneerVersion;
        }

        public HydroneerVersion HydroneerVersion { get; }

        public List<ProjectConfig> Projects { get; }

        public Guid? DefaultProject { get; }

        public List<GuidConfigItem> Guids { get; }
        
        public List<UIDsConfigItem> UIDs { get; }
    }
}
