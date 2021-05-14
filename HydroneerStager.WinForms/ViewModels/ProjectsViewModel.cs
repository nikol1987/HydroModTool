using HydroneerStager.WinForms.Data;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HydroneerStager.WinForms.ViewModels
{
    public sealed class ProjectsViewModel : ReactiveObject
    {
        private readonly ApplicationStore _applicationStore;
        private readonly AddProjectView _addProjectView;

        public ProjectsViewModel(ApplicationStore applicationStore, AddProjectView addProjectView)
        {
            _applicationStore = applicationStore;
            _addProjectView = addProjectView;


            _applicationStore
                .WhenAnyValue(appStore => appStore.AppState)
                .Subscribe(newState => {
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
            set {
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

        internal ReactiveCommand<string, Unit> ExecuteStripMenuCommand;
        private void ExecuteStripMenu(string stripItemName)
        {
            switch (stripItemName)
            {
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
                default:
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
            var projects = new List<ProjectModel>(_applicationStore.AppState.Projects.Where(p => p.Id != projectId));
            _applicationStore.AppState.Projects = projects;
            await _applicationStore.ReloadState();
        }

        internal ReactiveCommand<IList<Guid>, Unit> DeleteAssetsCommand;
        private async void DeleteAssets(IList<Guid> assetsId)
        {
            var project = _applicationStore.AppState.Projects.Where(p => p.Id == SelectedProject).First();

            project.Items = project.Items.Where(item => !assetsId.Contains(item.Id)).ToList();
            _applicationStore.AppState.Projects = _applicationStore.AppState.Projects;

            await _applicationStore.ReloadState();
        }

        internal ReactiveCommand<Unit, Unit> AddAssetsCommand;
        private async void AddAssets()
        {
            var project = _applicationStore.AppState.Projects.Where(p => p.Id == SelectedProject).First();

            var files = await ChooseFilesHelper("Select assets", project.Path);

            if (files.Count == 0)
            {
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

            DeleteProject(Guid.Empty);
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