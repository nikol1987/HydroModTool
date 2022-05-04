using System;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using HydroModTools.Client.Wpf.DTOs;
using HydroModTools.Client.Wpf.Enums;
using HydroModTools.Client.Wpf.Validators;
using HydroModTools.Contracts.Services;
using ReactiveUI;
using Application = System.Windows.Application;
using MessageBox = HandyControl.Controls.MessageBox;

namespace HydroModTools.Client.Wpf.ViewModels
{
    internal class EditProjectViewModel : ReactiveObject
    {
        public event Action OnRequestClose = delegate {  };
        
        private readonly IConfigurationService _configurationService;
        private readonly IProjectsService _projectsService;

        public EditProjectViewModel(IConfigurationService configurationService, IProjectsService projectsService)
        {
            _configurationService = configurationService;
            _projectsService = projectsService;
            
            SaveProjectInfoCommand = ReactiveCommand.Create(SaveProjectInfo);
            SelectAssetsDirCommand = ReactiveCommand.Create<SelectDirMode>(SelectAssetsDir);
        }
        
        private string _projectName = string.Empty;
        public string ProjectName
        {
            get => _projectName;
            set => this.RaiseAndSetIfChanged(ref _projectName, value);
        }
        
        private string _projectIndex = "500";
        public string ProjectIndex
        {
            get => _projectIndex;
            set => this.RaiseAndSetIfChanged(ref _projectIndex, value);
        }
        
        private string _cookedAssetsDir = string.Empty;
        public string CookedAssetsDir
        {
            get => _cookedAssetsDir;
            set => this.RaiseAndSetIfChanged(ref _cookedAssetsDir, value);
        }
        
        private string _distDir = string.Empty;
        public string DistDir
        {
            get => _distDir;
            set => this.RaiseAndSetIfChanged(ref _distDir, value);
        }
        
        private Guid _projectId = Guid.Empty;
        public Guid ProjectId
        {
            get => _projectId;
            set => this.RaiseAndSetIfChanged(ref _projectId, value);
        }
        
        public ReactiveCommand<Unit, Unit> SaveProjectInfoCommand;
        private void SaveProjectInfo()
        {
            Task.Run(() =>
            {
                var validator = new AddEditProjectValidator();
                var validationResult =
                    validator.Validate(new AddEditProjectDto(ProjectName, ProjectIndex, CookedAssetsDir, DistDir));

                if (!validationResult.IsValid)
                {
                    var errors = string.Join('\n', validationResult.Errors
                        .Select(e => e.ErrorMessage));

                    MessageBox.Show(errors, "Validation Errors", MessageBoxButton.OK, MessageBoxImage.Error);
                    
                    return;
                }
                
                var projectIndexShort =short.Parse(ProjectIndex);
                    
                if (ProjectId == Guid.Empty)
                {
                    _projectsService.AddProject(Guid.NewGuid(), ProjectName, projectIndexShort, CookedAssetsDir, DistDir);
                    OnRequestClose.Invoke();
                    
                    return;
                }

                _projectsService.EditProject(ProjectId, ProjectName, projectIndexShort, CookedAssetsDir, DistDir);
                
                OnRequestClose.Invoke();
            });
        }
        
        public ReactiveCommand<SelectDirMode, Unit> SelectAssetsDirCommand;
        private void SelectAssetsDir(SelectDirMode mode)
        {
            Task.Run(() =>
            {
                switch (mode)   
                {
                    case SelectDirMode.SelectCookedAssetsDir:
                        ChooseFolderHelper("Select cooked assets folder", path =>
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                CookedAssetsDir = path;
                            });
                        });
                        
                        break;
                    case SelectDirMode.SelectOutputDir:
                        ChooseFolderHelper("Select output assets folder", path =>
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                DistDir = path;
                            });
                        });
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(mode), mode, @"Invalid asset select mode.");
                }
            });
        }

        public void SetProject(Guid projectId)
        {
            var project = _configurationService.GetAsync()
                .Result
                .Projects
                .First(project => project.Id == projectId);

            ProjectId = project.Id;
            ProjectName = project.Name;
            ProjectIndex = project.ModIndex.ToString();
            CookedAssetsDir = project.Path;
            DistDir = project.OutputPath;
        }
        
        private static void ChooseFolderHelper(string title, Action<string> action)
        {
            var result = new StringBuilder();
            var thread = new Thread(obj =>
            {
                var builder = obj as StringBuilder;
                using var dialog = new FolderBrowserDialog();
                dialog.Description = title;
                dialog.RootFolder = Environment.SpecialFolder.MyComputer;
                
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    builder!.Append(dialog.SelectedPath);
                }
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start(result);

            while (thread.IsAlive)
            {
                Thread.Sleep(100);
            }

            action.Invoke(result.ToString());
        }
    }
}