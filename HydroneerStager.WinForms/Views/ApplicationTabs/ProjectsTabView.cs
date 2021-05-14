using HydroneerStager.WinForms.ViewModels;
using HydroneerStager.WinForms.Views.ApplicationTabs.ProjectTabs;
using ReactiveUI;
using System.Windows.Forms;

namespace HydroneerStager.WinForms.Views.ApplicationTabs
{
    public partial class ProjectsTabView : UserControl, IViewFor<ProjectTabViewModel>
    {
        public ProjectsTabView(
            ProjectsView projectView,
            ProjectTabViewModel projectsTabViewModel)
        {
            InitializeComponent();

            projectView.Dock = DockStyle.Fill;
            this.kryptonPage1.Controls.Add(projectView);

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
