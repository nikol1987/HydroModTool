using HydroModTools.Contracts.Services;
using HydroModTools.WinForms.Data;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive;
using System.Windows.Forms;

namespace HydroModTools.WinForms.ViewModels
{
    public sealed class AddProjectViewModel : ReactiveObject
    {
        private readonly IProjectsService _projectService;
        private readonly IConfigurationService _configurationService;

        public AddProjectViewModel(IProjectsService projectService, IConfigurationService configurationService)
        {
            AddProjectCommand = ReactiveCommand.Create<Form>(AddProject);
            _projectService = projectService;
            _configurationService = configurationService;
        }

        public AddProjectViewModel(IProjectsService projectService, IConfigurationService configurationService, Guid projectId):
            this(projectService, configurationService)
        {
            var project = ApplicationStore.Store.Projects.First(p => p.Id == projectId);

            Id = project.Id;
            Name = project.Name;
            Path = project.Path;
            OutputPath = project.OutputPath;

            editMode = true;
        }

        private bool editMode = false;

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
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        private string _modIndex = "500";
        internal string ModIndex
        {
            get => _modIndex;
            set => this.RaiseAndSetIfChanged(ref _modIndex, value);
        }

        private string _path;
        internal string Path
        {
            get => _path;
            set => this.RaiseAndSetIfChanged(ref _path, value);
        }

        private string _outputPath;
        internal string OutputPath
        {
            get => _outputPath;
            set => this.RaiseAndSetIfChanged(ref _outputPath, value);
        }

        public ReactiveCommand<Form, Unit> AddProjectCommand;
        public async void AddProject(Form form)
        {
            if (editMode)
            {
                await _projectService.EditProject(Id, Name, short.Parse(ModIndex), Path, OutputPath);
            } else
            {
                await _projectService.AddProject(Id, Name, short.Parse(ModIndex), Path, OutputPath);
            }

            var config = await _configurationService.GetAsync();
            await ApplicationStore.RefreshStore(config);

            form.Dispose();
            form.Close();
        }
    }
}