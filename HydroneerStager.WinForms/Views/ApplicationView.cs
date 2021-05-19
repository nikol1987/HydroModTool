using HydroneerStager.WinForms.ViewModels;
using HydroneerStager.WinForms.Views.ApplicationTabs;
using ReactiveUI;
using System;
using System.Drawing;
using System.Reactive.Linq;
using System.Windows.Forms;

namespace HydroneerStager.WinForms.Views
{
    public partial class ApplicationView : Form, IViewFor<ApplicationViewModel>
    {
        public ApplicationView(
            ModInstallerTabView modInstallerTabView,
            ApplicationViewModel applicationViewModel,
            AboutTabView aboutTabView,
            ProjectsTabView projectTabView)
        {
            Utilities.SetupFonts();

            InitializeComponent();

            ViewModel = applicationViewModel;

            modInstallerTabView.Dock = DockStyle.Fill;
            kryptonPage1.Controls.Add(modInstallerTabView);

            projectTabView.Dock = DockStyle.Fill;
            kryptonPage2.Controls.Add(projectTabView);

            aboutTabView.Dock = DockStyle.Fill;
            kryptonPage3.Controls.Add(aboutTabView);

            this.WhenActivated(d =>
            {
                d(this.OneWayBind(ViewModel, vm => vm.ApplicationTitle, v => v.Text));
                d(this.OneWayBind(ViewModel, vm => vm.Tab1Title, v => v.kryptonPage1.Text));
                d(this.OneWayBind(ViewModel, vm => vm.Tab2Title, v => v.kryptonPage2.Text));
                d(this.OneWayBind(ViewModel, vm => vm.Tab3Title, v => v.kryptonPage3.Text));

                d(this.kryptonNavigator1
                    .WhenAnyValue(nav => nav.SelectedPage)
                    .Subscribe(page => {
                        var pageName = (page).Text;

                        if (pageName == ViewModel.Tab1Title)
                        {
                            this.Size = new Size(1035, 720);
                            this.FormBorderStyle = FormBorderStyle.FixedDialog;
                        }
                        else
                        {
                            this.FormBorderStyle = FormBorderStyle.Sizable;
                        }
                    }));
            });

        }

        public ApplicationViewModel ViewModel { get; set; }

        object IViewFor.ViewModel { get => ViewModel; set => ViewModel = (ApplicationViewModel)value; }
    }
}
