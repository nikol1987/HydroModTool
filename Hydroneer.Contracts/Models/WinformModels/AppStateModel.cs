using ReactiveUI;
using System;
using System.Collections.Generic;

namespace HydroneerStager.Contracts.Models.WinformModels
{
    public sealed class AppStateModel : ReactiveObject
    {
        private Guid? _selectedProject;
        public Guid? SelectedProject
        {
            get => _selectedProject;
            set
            {
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

        private IReadOnlyCollection<GuidModel> _guids;
        public IReadOnlyCollection<GuidModel> Guids
        {
            get => _guids;
            set
            {
                this.RaiseAndSetIfChanged(ref _guids, value);
                this.RaisePropertyChanged();
            }
        }
    }
}
