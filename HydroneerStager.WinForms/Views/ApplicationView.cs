using HydroneerStager.WinForms.ViewModels;
using HydroneerStager.WinForms.Views.ApplicationTabs;
using ReactiveUI;
using System.Windows.Forms;

namespace HydroneerStager.WinForms.Views
{
    public partial class ApplicationView : Form, IViewFor<ApplicationViewModel>
    {
        public ApplicationView(
            ApplicationViewModel applicationViewModel,
            ProjectsTabView projectTabView)
        {
            InitializeComponent();

            projectTabView.Dock = DockStyle.Fill;
            this.kryptonPage2.Controls.Add(projectTabView);

            this.WhenActivated(d =>
            {
                d(this.OneWayBind(ViewModel, vm => vm.ApplicationTitle, v => v.Text));
                d(this.OneWayBind(ViewModel, vm => vm.Tab1Title, v => v.kryptonPage1.Text));
                d(this.OneWayBind(ViewModel, vm => vm.Tab2Title, v => v.kryptonPage2.Text));
                d(this.OneWayBind(ViewModel, vm => vm.Tab3Title, v => v.kryptonPage3.Text));
            });

            ViewModel = applicationViewModel;
        }

        public ApplicationViewModel ViewModel { get; set; }

        object IViewFor.ViewModel { get => ViewModel; set => ViewModel = (ApplicationViewModel)value; }
    }
}
