using HydroModTools.Contracts.Enums;
using HydroModTools.Contracts.Models;
using HydroModTools.Contracts.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HydroModTools.Client.Wpf.DI;
using HydroModTools.Client.Wpf.Views;

namespace HydroModTools.Client.Wpf.ControlModels
{
    internal sealed class ProjectsTabControlModel : ReactiveObject
    {
        public event Action OnAssetsUpdate = delegate {  };
        
        private readonly IAppService _appService;
        private readonly IConfigurationService _configurationService;
        private readonly IProjectsService _projectsService;

        public ProjectsTabControlModel(IAppService appService, IConfigurationService configurationService, IProjectsService projectsService)
        {
            _appService = appService;
            _configurationService = configurationService;
            _projectsService = projectsService;

            StartGameCommand = ReactiveCommand.Create(StartGame);
            SetGameVersionCommand = ReactiveCommand.Create<int>(SetGameVersion);
            AddProjectFilesCommand = ReactiveCommand.Create(AddProjectFiles);
            DeleteAssetsCommand = ReactiveCommand.Create<IReadOnlyCollection<Guid>>(DeleteAssets);
            EditProjectCommand = ReactiveCommand.Create(EditProject);
            DeleteProjectCommand = ReactiveCommand.Create(DeleteProject);
            AddProjectCommand = ReactiveCommand.Create(AddProject);

            _configurationService
                .GetAsync()
                .ContinueWith(configTask => {
                    var config = configTask.Result;

                    SelectedGame = config.HydroneerVersion;
                    ProjectList = config.Projects;
                });
        }

        private Guid _selectedProject = Guid.Empty;
        public Guid SelectedProject
        {
            get => _selectedProject;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedProject, value);
            }
        }

        private IReadOnlyCollection<ProjectModel> _projectList = new List<ProjectModel>();
        public IReadOnlyCollection<ProjectModel> ProjectList
        {
            get => _projectList;
            set
            {
                this.RaiseAndSetIfChanged(ref _projectList, value);
            }
        }

        private HydroneerVersion _selectedVersion = HydroneerVersion.HydroneerLegacy;
        public HydroneerVersion SelectedGame
        {
            get => _selectedVersion;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedVersion, value);
            }
        }
        
        public ReactiveCommand<int, Unit> SetGameVersionCommand;
        private void SetGameVersion(int gameIndex)
        {
            _appService.SetGameVersion((HydroneerVersion)gameIndex);
        }

        public ReactiveCommand<Unit, Unit> StartGameCommand;
        private void StartGame()
        {
            _appService.StartGame();
        }

        public ReactiveCommand<Unit, Unit> AddProjectFilesCommand;
        private void AddProjectFiles()
        {
            Task.Run(async () =>
            {
                var project = ProjectList.First(p => p.Id == SelectedProject);

                var files = await ChooseFilesHelper("Select assets", project.Path);

                await _projectsService.AddAssets(SelectedProject, files.ToList());

                ReloadAssets();
            });
        }
        
        public ReactiveCommand<Unit, Unit> DeleteProjectCommand;
        private void DeleteProject()
        {
            Task.Run(async () => {
                var project = ProjectList.First(p => p.Id == SelectedProject);

                await _projectsService.DeleteProject(project.Id);
                
                ReloadAssets();
            });
        }
        
        public ReactiveCommand<Unit, Unit> EditProjectCommand;
        private void EditProject()
        {
            Task.Run(() => {
                var project = ProjectList.First(p => p.Id == SelectedProject);

                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    var editControl = WpfFactory.CreateWindow<EditProjectView>();
                    editControl.SetProject(project.Id);
                    editControl.ShowDialog();
                });
                
                ReloadAssets();
            });
        }
        
        public ReactiveCommand<Unit, Unit> AddProjectCommand;
        private void AddProject()
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                var editControl = WpfFactory.CreateWindow<EditProjectView>();
                editControl.ShowDialog();
                
                ReloadAssets();
            });
        }
        
        public ReactiveCommand<IReadOnlyCollection<Guid>, Unit> DeleteAssetsCommand;
        private void DeleteAssets(IReadOnlyCollection<Guid> assetsIds)
        {
            Task.Run(async () => {
                var project = ProjectList.First(p => p.Id == SelectedProject);

                await _projectsService.RemoveAssets(project.Id, assetsIds);
                
                ReloadAssets();
            });
        }

        private void ReloadAssets()
        {
            _configurationService
                .GetAsync()
                .ContinueWith((configTask) => {
                    var config = configTask.Result;

                    ProjectList = config.Projects;
                    
                    OnAssetsUpdate.Invoke();
                });
        }

        private async Task<IList<string>> ChooseFilesHelper(string title, string rootPath)
        {
            var result = new List<string>();
            var thread = new Thread(obj =>
            {

                var mbresult = MessageBox.Show("Do you want to select a file? ('No' to select folder)", "Select Files?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (mbresult == DialogResult.Cancel)
                {
                    return;
                }

                var files = (List<string>)obj!;

                if (mbresult == DialogResult.Yes)
                {
                    using (var dialog = new OpenFileDialog())
                    {
                        dialog.Title = title;
                        dialog.Multiselect = true;
                        dialog.Filter = "UE Assets|*.uasset;*.uexp;*.ubulk;*.ini;*.bin;*.umap;*.uplugin;*.uproject";
                        dialog.InitialDirectory = rootPath;
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            files.AddRange(dialog.FileNames);
                        }
                    }

                    return;
                }

                using (var dialog = new FolderBrowserDialog())
                {
                    dialog.UseDescriptionForTitle = true;
                    dialog.Description = title;
                    dialog.SelectedPath = rootPath;
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        files.AddRange(Directory.GetFiles(dialog.SelectedPath, "*.uasset", SearchOption.AllDirectories).ToList());
                        files.AddRange(Directory.GetFiles(dialog.SelectedPath, "*.uexp", SearchOption.AllDirectories).ToList());
                        files.AddRange(Directory.GetFiles(dialog.SelectedPath, "*.ubulk", SearchOption.AllDirectories).ToList());
                        files.AddRange(Directory.GetFiles(dialog.SelectedPath, "*.ini", SearchOption.AllDirectories).ToList());
                        files.AddRange(Directory.GetFiles(dialog.SelectedPath, "*.bin", SearchOption.AllDirectories).ToList());
                        files.AddRange(Directory.GetFiles(dialog.SelectedPath, "*.umap", SearchOption.AllDirectories).ToList());
                        files.AddRange(Directory.GetFiles(dialog.SelectedPath, "*.uplugin", SearchOption.AllDirectories).ToList());
                        files.AddRange(Directory.GetFiles(dialog.SelectedPath, "*.uproject", SearchOption.AllDirectories).ToList());
                    }
                }
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start(result);

            while (thread.IsAlive)
            {
                Thread.Sleep(100);
            }

            return await Task.FromResult(result);
        }
    }
}
