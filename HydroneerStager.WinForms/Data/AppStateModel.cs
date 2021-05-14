using ReactiveUI;
using System;
using System.Collections.Generic;

namespace HydroneerStager.WinForms.Data
{
    public sealed class AppStateModel : ReactiveObject
    {
        private Guid? _selectedProject;
        public Guid? SelectedProject
        {
            get => _selectedProject;
            set {
                this.RaiseAndSetIfChanged(ref _selectedProject, value);
                this.RaisePropertyChanged();
            }
        }

        private IReadOnlyCollection<ProjectModel> _projects;
        public IReadOnlyCollection<ProjectModel> Projects
        {
            get => _projects;
            set
            {
                this.RaiseAndSetIfChanged(ref _projects, value);
                this.RaisePropertyChanged();
            }
        }
    }
}
