using HydroModTools.Contracts.Services;
using HydroModTools.WinForms.Data;
using ReactiveUI;
using System;
using System.Reactive;
using System.Windows.Forms;

namespace HydroModTools.WinForms.ViewModels
{
    public sealed class AddProjectViewModel : ReactiveObject
    {
        private readonly IProjectsService _projectService;

        public AddProjectViewModel(IProjectsService projectService)
        {
            AddProjectCommand = ReactiveCommand.Create<Form>(AddProject);
            _projectService = projectService;        }

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
            await _projectService.AddProject(Id, Name, Path, OutputPath);
            await ApplicationStore.RefreshStore();
            form.Close();
        }
    }
}