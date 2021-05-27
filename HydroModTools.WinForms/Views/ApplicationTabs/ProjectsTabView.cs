using HydroModTools.Winforms.Views.ApplicationTabs.ProjectTabs;
using HydroModTools.WinForms.ViewModels;
using ReactiveUI;
using System.Windows.Forms;

namespace HydroModTools.WinForms.Views.ApplicationTabs
{
    public partial class ProjectsTabView : UserControl, IViewFor<ProjectTabViewModel>
    {
        public ProjectsTabView(
            ProjectsView projectView,
            ProjectTabViewModel projectsTabViewModel,
            GuidsView guidsView)
        {
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

            ViewModel = projectsTabViewModel;
        }

        public ProjectTabViewModel ViewModel { get; set; }

        object IViewFor.ViewModel { get => ViewModel; set => ViewModel = (ProjectTabViewModel)value; }
    }
}
