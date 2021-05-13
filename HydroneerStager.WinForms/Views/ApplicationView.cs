using HydroneerStager.WinForms.ViewModels;
using ReactiveUI;
using System.Windows.Forms;

namespace HydroneerStager.WinForms.Views
{
    public partial class ApplicationView : Form, IViewFor<ApplicationViewModel>
    {
        public ApplicationView()
        {
            InitializeComponent();

            this.WhenActivated(d => {
                d(this.OneWayBind(ViewModel, vm => vm.ApplicationTitle, v => v.Text));
                d(this.OneWayBind(ViewModel, vm => vm.Tab1Title, v => v.kryptonPage1.Text));
                d(this.OneWayBind(ViewModel, vm => vm.Tab2Title, v => v.kryptonPage2.Text));
                d(this.OneWayBind(ViewModel, vm => vm.Tab3Title, v => v.kryptonPage3.Text));

            });

            ViewModel = new ApplicationViewModel();
        }

        public ApplicationViewModel ViewModel { get; set; }

        object IViewFor.ViewModel { get => ViewModel; set => ViewModel = (ApplicationViewModel)value; }
    }
}
