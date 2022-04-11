using ReactiveUI;
using System.Windows.Forms;
using System;
using System.Reactive.Linq;
using HydroModTools.Contracts.Services;
using System.Threading.Tasks;
using HydroModTools.Client.WinForms.ViewModels;

namespace HydroModTools.Winforms.Client.Views.ApplicationTabs
{
    public partial class AboutTabView : UserControl, IViewFor<AboutTabViewModel>
    {
        private WebBrowser _webControl;

        public AboutTabView(IAppService appService)
        {
            ViewModel = new AboutTabViewModel(appService);

            InitializeComponent();

            _webControl = new WebBrowser()
            {
                Dock = DockStyle.Fill
            };
            Controls.Add(_webControl);

            this.WhenActivated(d => {
                d(ViewModel
                    .WhenAnyValue(vm => vm.AboutHtml)
                    .Subscribe(async aboutHtml => {
                        await Task.Delay(2500);

                        _webControl.DocumentText = aboutHtml;
                        await Task.Delay(2500);

                        _webControl.AllowNavigation = false;
                    }));
            });
                
        }

        public AboutTabViewModel ViewModel { get; set; }

        object IViewFor.ViewModel { get => ViewModel; set => ViewModel = (AboutTabViewModel)value; }
    }
}
