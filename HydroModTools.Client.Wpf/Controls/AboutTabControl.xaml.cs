using HydroModTools.Client.Wpf.ControlModels;
using HydroModTools.Client.Wpf.DI;
using ReactiveUI;
using System.Reactive.Linq;
using System.Windows.Controls;
using System;

namespace HydroModTools.Client.Wpf.Controls
{
    internal partial class AboutTabControl : UserControl, IViewFor<AboutTabControlModel>
    {
        public AboutTabControl()
        {
            ViewModel = WpfFactory.CreateViewModel<AboutTabControlModel>();

            InitializeComponent();

            this.WhenActivated(d =>
            {
                ViewModel
                    .WhenAnyValue(vm => vm.AboutMd)
                    .ObserveOn(Dispatcher)
                    .Subscribe(aboutMd =>
                    {
                        MdPanel.Children.Clear();

                        var mrkDown = new Markdown.Xaml.Markdown();
                        var mdDocument = mrkDown.Transform(aboutMd);

                        var mdViewer = new FlowDocumentScrollViewer();
                        mdViewer.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                        mdViewer.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                        mdViewer.Margin = new System.Windows.Thickness(5);
                        mdViewer.Document = mdDocument;
                        mdViewer.Zoom = 80.0d;

                        MdPanel.Children.Add(mdViewer);
                    });
            });
        }

        object? IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (AboutTabControlModel)value!;
        }

        public AboutTabControlModel? ViewModel { get; set; }
    }
}
