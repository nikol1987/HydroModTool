using HydroModTools.Contracts.Services;
using HydroModTools.WinForms.DTOs;
using HydroModTools.WinForms.Validators;
using HydroModTools.WinForms.ViewModels;
using ReactiveUI;
using System;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace HydroModTools.Winforms.Views.ApplicationTabs.ProjectTabs
{
    public partial class AddProjectView : UserControl, IViewFor<AddProjectViewModel>
    {
        private readonly IProjectsService _projectsService;
        private readonly IConfigurationService _configurationService;

        public AddProjectView(IProjectsService projectsService, IConfigurationService configurationService)
        {
            _projectsService = projectsService;
            _configurationService = configurationService;

            ViewModel = new AddProjectViewModel(projectsService, configurationService);

            InitializeComponent();

            this.WhenActivated(d =>
            {
                d(this.Bind(ViewModel, vm => vm.Name, v => v.projectNameTextBox.Text));
                d(this.Bind(ViewModel, vm => vm.ModIndex, v => v.projectIndexTextBox.Text));
                d(this.Bind(ViewModel, vm => vm.Path, v => v.cookedAssetsDirTextBox.Text));
                d(this.Bind(ViewModel, vm => vm.OutputPath, v => v.outputPathDirTextBox.Text));
            });
        }

        private static void ChooseFolderHelper(string title, Action<string> action)
        {
            var result = new StringBuilder();
            var thread = new Thread(obj =>
            {
                var builder = (StringBuilder)obj;
                using (var dialog = new FolderBrowserDialog())
                {
                    dialog.Description = title;
                    dialog.RootFolder = Environment.SpecialFolder.MyComputer;
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        builder.Append(dialog.SelectedPath);
                    }
                }
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start(result);

            while (thread.IsAlive)
            {
                Thread.Sleep(100);
            }

            action.Invoke(result.ToString()); ;
        }

        public void SetEditMode(Guid projectGuid)
        {
            ViewModel = new AddProjectViewModel(_projectsService, _configurationService, projectGuid);
        }

        public AddProjectViewModel ViewModel { get; set; }

        object IViewFor.ViewModel { get => ViewModel; set => ViewModel = (AddProjectViewModel)value; }

        private void cancel_Click(object sender, EventArgs e)
        {
            FindForm().Close();
        }

        private async void submit_Click(object sender, EventArgs e)
        {
            var validator = new AddProjectValidator();
            var validationResult = validator.Validate(new AddProjectDto(ViewModel.Name, ViewModel.ModIndex, ViewModel.Path, ViewModel.OutputPath));

            var validationErrors = validationResult.Errors;

            if (!validationResult.IsValid)
            {
                MessageBox.Show(string.Join('\n', validationErrors), "Validation Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            await ViewModel.AddProjectCommand.Execute(ParentForm);
        }

        private void selectCookedDirBtn_Click(object sender, EventArgs e)
        {
            ChooseFolderHelper("Select Cooked assets folder", (path) =>
            {
                cookedAssetsDirTextBox.Text = path;
            });
        }

        private void outputPathDirBtn_Click(object sender, EventArgs e)
        {
            ChooseFolderHelper("Select output assets folder", (path) =>
            {
                outputPathDirTextBox.Text = path;
            });
        }
    }
}
