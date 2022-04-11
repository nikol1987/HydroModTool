using HydroModTools.Wpf.ControlModels;
using HydroModTools.Wpf.DI;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Windows.Controls;
using ReactiveMarbles.ObservableEvents;

namespace HydroModTools.Wpf.Controls
{
    internal partial class ModSegmentControl : UserControl, IViewFor<ModSegmentControlModel>
    {
        public ModSegmentControl()
        {
            ViewModel = WpfFactory.CreateViewModel<ModSegmentControlModel>();

            InitializeComponent();

            this.WhenActivated((d) => {
                this
                    .OneWayBind(ViewModel, vm => vm.Name, v => v.ModNameLabel.Text)
                    .DisposeWith(d);
                this
                    .OneWayBind(ViewModel, vm => vm.Author, v => v.ModAuthorLabel.Text)
                    .DisposeWith(d);
                this
                    .OneWayBind(ViewModel, vm => vm.Description, v => v.ModDescriptionLabel.Text)
                    .DisposeWith(d);
                this
                    .OneWayBind(ViewModel, vm => vm.CanDownload, v => v.DownloadMod.IsEnabled)
                    .DisposeWith(d);
                this
                     .OneWayBind(ViewModel, vm => vm.CanRemove, v => v.RemoveMod.IsEnabled)
                    .DisposeWith(d);

                DownloadMod
                    .Events()
                    .Click
                    .InvokeCommand(ViewModel.DownloadModCommand)
                    .DisposeWith(d);

                RemoveMod
                    .Events()
                    .Click
                    .InvokeCommand(ViewModel.RemoveModCommand)
                    .DisposeWith(d);
            });
        }

        object? IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (ModSegmentControlModel)value!;
        }

        public ModSegmentControlModel? ViewModel { get; set; }
    }
}
