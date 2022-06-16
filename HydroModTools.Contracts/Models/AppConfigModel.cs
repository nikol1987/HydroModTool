using HydroModTools.Contracts.Enums;
using System;
using System.Collections.Generic;

namespace HydroModTools.Contracts.Models
{
    public sealed class AppConfigModel
    {
        public AppConfigModel(
            IReadOnlyCollection<ProjectModel> projects,
            Guid? defaultProject,
            HydroneerVersion hydroneerVersion,
            IReadOnlyCollection<GuidItemModel> guids,
            IReadOnlyCollection<UIDItemModel> uiDs)
        {
            Projects = projects;
            DefaultProject = defaultProject;
            HydroneerVersion = hydroneerVersion;
            Guids = guids;
            UIDs = uiDs;
        }

        public HydroneerVersion HydroneerVersion { get; }

        public IReadOnlyCollection<ProjectModel> Projects { get; }

        public Guid? DefaultProject { get; }

        public IReadOnlyCollection<GuidItemModel> Guids { get; }
        
        public IReadOnlyCollection<UIDItemModel> UIDs { get; }
    }
}
