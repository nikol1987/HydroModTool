using Hydroneer.Contracts.Models;
using Hydroneer.Contracts.WinFormsServices;
using HydroneerStager.Contracts.Models.WinformModels;
using HydroneerStager.WinForms.Data;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HydroneerStager.WinForms.ViewModels
{
    public sealed class ProjectsViewModel : ReactiveObject
    {
        private readonly ApplicationStore _applicationStore;
        private readonly AddProjectView _addProjectView;
        private readonly IProjectsService _projectsService;

        public ProjectsViewModel(ApplicationStore applicationStore, AddProjectView addProjectView, IProjectsService projectsService)
        {
            _applicationStore = applicationStore;
            _addProjectView = addProjectView;
            _projectsService = projectsService;

            _applicationStore
                .WhenAnyValue(appStore => appStore.AppState)
                .Subscribe(newState =>
                {
                    _selectedProject = newState.SelectedProject ?? Guid.Empty;
                    SetProjects();
                    SetItems(_selectedProject);
                });


            ExecuteStripMenuCommand = ReactiveCommand.Create<string>(ExecuteStripMenu);
            SelectProjectCommand = ReactiveCommand.Create<Guid>(SelectProject);
            DeleteProjectCommand = ReactiveCommand.Create<Guid>(DeleteProject);
            DeleteAssetsCommand = ReactiveCommand.Create<IList<Guid>>(DeleteAssets);
            AddAssetsCommand = ReactiveCommand.Create(AddAssets);
        }

        private void SetProjects()
        {
            var projects = _applicationStore.AppState.Projects.Select(p => KeyValuePair.Create(p.Id, p.Name)).ToArray();
            _projects.DataSource = projects;
        }

        private void SetItems(Guid selectedProject)
        {
            var project = _applicationStore.AppState.Projects.Where(p => p.Id == selectedProject).FirstOrDefault();

            if (project == null)
            {
                return;
            }

            ProjectItems = Utilities.BuildFileStruture(project.Items).FirstNode;
        }

        private Guid _selectedProject;
        internal Guid SelectedProject
        {
            get => _selectedProject;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedProject, value);
            }
        }

        private BindingSource _projects = new BindingSource();
        internal BindingSource Projects
        {
            get => _projects;
            set => this.RaiseAndSetIfChanged(ref _projects, value);
        }

        private TreeNode _projectItems = new TreeNode();
        internal TreeNode ProjectItems
        {
            get => _projectItems;
            set => this.RaiseAndSetIfChanged(ref _projectItems, value);
        }

        private ProgressbarStateModel _progressbarState = new ProgressbarStateModel(0, "Ready");
        internal ProgressbarStateModel ProgressBarState
        {
            get => _progressbarState;
            set => this.RaiseAndSetIfChanged(ref _progressbarState, value);
        }

        internal ReactiveCommand<string, Unit> ExecuteStripMenuCommand;
        private async void ExecuteStripMenu(string stripItemName)
        {
            var project = _applicationStore.AppState.Projects.Where(p => p.Id == SelectedProject).FirstOrDefault();

            var timer = new System.Windows.Forms.Timer()
            {
                Enabled = true,
                Interval = 500
            };

            var processInfo = new ProcessStartInfo()
            {
                UseShellExecute = true,
                FileName = "steam://rungameid/1106840"
            };

            switch (stripItemName)
            {
                case "refresh":
                    ProgressBarState = new ProgressbarStateModel(10, $"Refreshing state");

                    await _applicationStore.ReloadState();

                    timer.Tick += (sender, ea) =>
                    {
                        _applicationStore.ReloadState();
                        ProgressBarState = new ProgressbarStateModel(0, "Ready");
                        timer.Stop();
                    };

                    timer.Start();

                    break;

                case "addProject":
                    var selectFolderForm = new Form
                    {
                        FormBorderStyle = FormBorderStyle.None
                    };

                    _addProjectView.Dock = DockStyle.Fill;


                    selectFolderForm.Size = _addProjectView.Size;
                    selectFolderForm.Controls.Add(_addProjectView);
                    selectFolderForm.ShowDialog();
                    break;

                case "stageProject":
                    ProgressBarState = new ProgressbarStateModel(10, $"Staging project {project.Name}");

                    await _projectsService.StageProject(project.Id, 10, 95, (progess) =>
                    {
                        ProgressBarState = progess;
                    });

                    ProgressBarState = new ProgressbarStateModel(100, "Project staged");


                    timer.Tick += (sender, ea) =>
                    {
                        _applicationStore.ReloadState();
                        ProgressBarState = new ProgressbarStateModel(0, "Ready");
                        timer.Stop();
                    };

                    timer.Start();
                    break;

                case "packageProject":
                    ProgressBarState = new ProgressbarStateModel(10, $"Packaging project {project.Name}");

                    await _projectsService.PackageProject(project.Id, 10, 95, (progess) =>
                    {
                        ProgressBarState = progess;
                    });

                    ProgressBarState = new ProgressbarStateModel(100, "Project packaged");

                    timer.Tick += (sender, ea) =>
                    {
                        _applicationStore.ReloadState();
                        ProgressBarState = new ProgressbarStateModel(0, "Ready");
                        timer.Stop();
                    };

                    timer.Start();

                    break;

                case "copyMod":
                    ProgressBarState = new ProgressbarStateModel(10, $"Copying project {project.Name}");

                    await _projectsService.CopyProject(project.Id, 10, 95, (progess) =>
                    {
                        ProgressBarState = progess;
                    });

                    ProgressBarState = new ProgressbarStateModel(100, "Project copied");

                    timer.Tick += (sender, ea) =>
                    {
                        _applicationStore.ReloadState();
                        ProgressBarState = new ProgressbarStateModel(0, "Ready");
                        timer.Stop();
                    };

                    timer.Start();

                    break;

                case "launchGame":
                    ProgressBarState = new ProgressbarStateModel(95, $"Starting Game");

                    Process.Start(processInfo);

                    timer.Tick += (sender, ea) =>
                    {
                        _applicationStore.ReloadState();
                        ProgressBarState = new ProgressbarStateModel(0, "Ready");
                        timer.Stop();
                    };

                    timer.Start();

                    break;

                case "devExpress":
                    ProgressBarState = new ProgressbarStateModel(10, $"Staging project {project.Name}");

                    await _projectsService.StageProject(project.Id, 10, 35, (progess) =>
                    {
                        ProgressBarState = progess;
                    });

                    ProgressBarState = new ProgressbarStateModel(36, $"Packaging project {project.Name}");

                    await _projectsService.PackageProject(project.Id, 36, 80, (progess) =>
                    {
                        ProgressBarState = progess;
                    });

                    ProgressBarState = new ProgressbarStateModel(81, $"Copying project {project.Name}");

                    await _projectsService.CopyProject(project.Id, 81, 90, (progess) =>
                    {
                        ProgressBarState = progess;
                    });

                    ProgressBarState = new ProgressbarStateModel(95, $"Starting Game");

                    Process.Start(processInfo);

                    timer.Tick += (sender, ea) =>
                    {
                        _applicationStore.ReloadState();
                        ProgressBarState = new ProgressbarStateModel(0, "Ready");
                        timer.Stop();
                    };

                    timer.Start();


                    break;

                default:

                    ProgressBarState = new ProgressbarStateModel(0, "Ready");

                    break;
            }
        }

        internal ReactiveCommand<Guid, Unit> SelectProjectCommand;
        private void SelectProject(Guid projectId)
        {
            _applicationStore.AppState.SelectedProject = projectId;
            SelectedProject = projectId;

            SetItems(projectId);
        }

        internal ReactiveCommand<Guid, Unit> DeleteProjectCommand;
        private async void DeleteProject(Guid projectId)
        {
            if (projectId == Guid.Empty)
            {
                ProgressBarState = new ProgressbarStateModel(0, "Ready");

                return;
            }

            var projects = new List<ProjectModel>(_applicationStore.AppState.Projects.Where(p => p.Id != projectId).ToList());

            ProgressBarState = new ProgressbarStateModel(10, $"Removing project {_applicationStore.AppState.Projects.Where(p => p.Id == projectId).First().Name}");

            _applicationStore.AppState.Projects = projects;
            _applicationStore.AppState.SelectedProject = null;
            ProjectItems = null;

            ProgressBarState = new ProgressbarStateModel(100, "Removed project");

            var timer = new System.Windows.Forms.Timer()
            {
                Enabled = true,
                Interval = 500
            };

            timer.Tick += (sender, ea) =>
            {
                _applicationStore.ReloadState();
                ProgressBarState = new ProgressbarStateModel(0, "Ready");
                timer.Stop();
            };

            timer.Start();


        }

        internal ReactiveCommand<IList<Guid>, Unit> DeleteAssetsCommand;
        private async void DeleteAssets(IList<Guid> assetsId)
        {
            var project = _applicationStore.AppState.Projects.Where(p => p.Id == SelectedProject).First();
            ProgressBarState = new ProgressbarStateModel(10, $"Removing project {project.Name}");

            project.Items = project.Items.Where(item => !assetsId.Contains(item.Id)).ToList();
            _applicationStore.AppState.Projects = _applicationStore.AppState.Projects;

            await _applicationStore.ReloadState();

            ProgressBarState = new ProgressbarStateModel(100, "Removed assets");

            var timer = new System.Windows.Forms.Timer()
            {
                Enabled = true,
                Interval = 500
            };

            timer.Tick += (sender, ea) =>
            {
                _applicationStore.ReloadState();
                ProgressBarState = new ProgressbarStateModel(0, "Ready");
                timer.Stop();
            };

            timer.Start();
        }

        internal ReactiveCommand<Unit, Unit> AddAssetsCommand;
        private async void AddAssets()
        {
            if (_applicationStore.AppState.Projects.Count == 0)
            {
                return;
            }

            var project = _applicationStore.AppState.Projects.Where(p => p.Id == SelectedProject).First();
            ProgressBarState = new ProgressbarStateModel(10, $"Add assets to project {project.Name}");

            var files = await ChooseFilesHelper("Select assets", project.Path);

            if (files.Count == 0)
            {

                ProgressBarState = new ProgressbarStateModel(0, "Ready");

                return;
            }

            var projectItems = new List<ProjectItemModel>(project.Items);

            foreach (var item in files)
            {
                var partialPath = item.Replace(project.Path, "");

                var contains = project.Items.Any(i => i.Name == Path.GetFileName(item) && i.Path == partialPath);

                if (contains)
                {
                    continue;
                }

                projectItems.Add(new ProjectItemModel(Guid.NewGuid(), Path.GetFileName(item), partialPath));
            }

            project.Items = projectItems;

            var projects = new List<ProjectModel>(_applicationStore.AppState.Projects.Where(p => p.Id != project.Id).ToList());
            projects.Add(project);

            _applicationStore.AppState.Projects = projects;

            ProgressBarState = new ProgressbarStateModel(100, "Added assets");

            var timer = new System.Windows.Forms.Timer()
            {
                Enabled = true,
                Interval = 500
            };

            timer.Tick += (sender, ea) =>
            {
                _applicationStore.ReloadState();
                ProgressBarState = new ProgressbarStateModel(0, "Ready");
                timer.Stop();
            };

            timer.Start();
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

                var files = (List<string>)obj;

                if (mbresult == DialogResult.Yes)
                {
                    using (var dialog = new OpenFileDialog())
                    {
                        dialog.Title = title;
                        dialog.Multiselect = true;
                        dialog.Filter = "UE Assets|*.uasset;*.uexp";
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
                    }
                }
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start(result);

            while (thread.IsAlive)
            {
                Thread.Sleep(100);
            }

            return result;
        }
    }
}