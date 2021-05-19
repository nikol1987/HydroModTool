using HydroneerStager.WinForms.ViewModels;
using ReactiveUI;
using System.Windows.Forms;
using System;
using System.Reactive.Linq;

namespace HydroneerStager.WinForms.Views.ApplicationTabs
{
    public partial class AboutTabView : UserControl, IViewFor<AboutTabViewModel>
    {
        private WebBrowser _webControl;


        public AboutTabView(AboutTabViewModel aboutTabViewModel)
        {
            InitializeComponent();

            ViewModel = aboutTabViewModel;

            _webControl = new WebBrowser()
            {
                Dock = DockStyle.Fill
            };
            this.Controls.Add(_webControl);

            this.WhenActivated(d => {
                d(ViewModel
                    .WhenAnyValue(vm => vm.AboutHtml)
                    .Subscribe(aboutHtml => {
                        _webControl.DocumentText = aboutHtml;
                    }));
            });
                
        }

        public AboutTabViewModel ViewModel { get; set; }

        object IViewFor.ViewModel { get => ViewModel; set => ViewModel = (AboutTabViewModel)value; }
    }
}
