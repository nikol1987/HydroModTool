using ReactiveUI;
using System.Reactive.Disposables;
using System.Windows.Controls;
using ReactiveMarbles.ObservableEvents;
using HydroModTools.DataAccess.Contracts.Models;
using HydroModTools.Client.Wpf.ControlModels;
using HydroModTools.Client.Wpf.DI;

namespace HydroModTools.Client.Wpf.Controls
{
    internal partial class ModSegmentControl : UserControl, IViewFor<ModSegmentControlModel>
    {
        public ModSegmentControl()
        {
            ViewModel = WpfFactory.CreateViewModel<ModSegmentControlModel>();

            InitializeComponent();

            this.WhenActivated((d) =>
            {
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

        public void SetModDetails(BridgepourModModel bridgepourMod)
        {
            ViewModel!.SetModInfo(
                bridgepourMod.Author,
                bridgepourMod.Description,
                bridgepourMod.Name,
                bridgepourMod.RibbonColor,
                bridgepourMod.RibbonText,
                bridgepourMod.Status,
                bridgepourMod.Url);
        }

        object? IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (ModSegmentControlModel)value!;
        }

        public ModSegmentControlModel? ViewModel { get; set; }
    }
}
