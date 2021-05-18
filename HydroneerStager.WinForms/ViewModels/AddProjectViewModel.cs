using Hydroneer.Contracts.WinFormsServices;
using HydroneerStager.Contracts.Models.WinformModels;
using HydroneerStager.WinForms.Data;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Windows.Forms;

namespace HydroneerStager.WinForms.ViewModels
{
    public sealed class AddProjectViewModel : ReactiveObject
    {
        private readonly IProjectsService _projectService;
        private readonly ApplicationStore _applicationStore;

        public AddProjectViewModel(IProjectsService projectService, ApplicationStore applicationStore)
        {
            AddProjectCommand = ReactiveCommand.Create<Form>(AddProject);
            _projectService = projectService;
            _applicationStore = applicationStore;
        }

        private Guid _id = Guid.NewGuid();
        internal Guid Id
        {
            get => _id;
            set
            {
                this.RaiseAndSetIfChanged(ref _id, value);
            }
        }

        private string _name;
        internal string Name
        {
            get => _name;
            set
            {
                this.RaiseAndSetIfChanged(ref _name, value);
            }
        }

        private string _path;
        internal string Path
        {
            get => _path;
            set
            {
                this.RaiseAndSetIfChanged(ref _path, value);
            }
        }

        private string _outputPath;
        internal string OutputPath
        {
            get => _outputPath;
            set
            {
                this.RaiseAndSetIfChanged(ref _outputPath, value);
            }
        }

        public ReactiveCommand<Form, Unit> AddProjectCommand;

        public async void AddProject(Form form)
        {
            var projects = new List<ProjectModel>(_applicationStore.AppState.Projects);
            var newProject = new ProjectModel(Id, Name, Path, OutputPath, new List<ProjectItemModel>());

            projects.Add(newProject);

            _applicationStore.AppState.Projects = projects;
            _applicationStore.AppState.SelectedProject = newProject.Id;
            await _applicationStore.ReloadState();
            form.Close();
        }
    }
}