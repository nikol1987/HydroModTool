using System;
using System.Collections.Generic;

namespace HydroModTools.Contracts.Models
{
    public sealed class AppConfigModel
    {
        public AppConfigModel(IReadOnlyCollection<ProjectModel> projects, Guid? defaultProject, IReadOnlyCollection<GuidItemModel> guids)
        {
            Projects = projects;
            DefaultProject = defaultProject;
            Guids = guids;
        }

        public IReadOnlyCollection<ProjectModel> Projects { get; }

        public Guid? DefaultProject { get; }

        public IReadOnlyCollection<GuidItemModel> Guids { get; }
    }
}
