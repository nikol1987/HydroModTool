using System.Collections.Generic;

namespace HydroneerStager.Contracts.Models.WinformModels
{
    internal sealed class ProjectListModel
    {
        public ProjectListModel(
            IReadOnlyList<ProjectModel> projects)
        {
            Projects = projects;
        }

        public IReadOnlyList<ProjectModel> Projects { get; }
    }
}
