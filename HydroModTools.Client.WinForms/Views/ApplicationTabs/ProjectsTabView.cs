using HydroModTools.Client.WinForms.ViewModels;
using HydroModTools.Winforms.Client.Views.ApplicationTabs.ProjectTabs;
using ReactiveUI;
using System.Windows.Forms;

namespace HydroModTools.Winforms.Client.Views.ApplicationTabs
{
    public partial class ProjectsTabView : UserControl, IViewFor<ProjectTabViewModel>
    {
        public ProjectsTabView(
            ProjectsView projectView,
            GuidsView guidsView)
        {

            ViewModel = new ProjectTabViewModel();

            InitializeComponent();

            projectView.Dock = DockStyle.Fill;
            this.kryptonPage1.Controls.Add(projectView);

            guidsView.Dock = DockStyle.Fill;
            this.kryptonPage2.Controls.Add(guidsView);

            this.WhenActivated(d =>
            {
                d(this.OneWayBind(ViewModel, vm => vm.Tab1Title, v => v.kryptonPage1.Text));
                d(this.OneWayBind(ViewModel, vm => vm.Tab2Title, v => v.kryptonPage2.Text));
            });
        }

        public ProjectTabViewModel ViewModel { get; set; }

        object IViewFor.ViewModel { get => ViewModel; set => ViewModel = (ProjectTabViewModel)value; }
    }
}
