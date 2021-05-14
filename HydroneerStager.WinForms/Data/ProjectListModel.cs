using System.Collections.Generic;

namespace HydroneerStager.WinForms.Data
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
