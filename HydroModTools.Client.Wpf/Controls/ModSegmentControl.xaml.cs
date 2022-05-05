using ReactiveUI;
using System.Reactive.Disposables;
using System.Windows.Controls;
using ReactiveMarbles.ObservableEvents;
using HydroModTools.DataAccess.Contracts.Models;
using HydroModTools.Client.Wpf.ControlModels;
using HydroModTools.Client.Wpf.DI;
using System.Reactive.Linq;
using System.Reactive;
using System.Windows.Media;

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
                ViewModel
                    .WhenAnyValue(vm => vm.CanDownload)
                    .ObserveOn(Dispatcher)
                    .BindTo(this, v => v.DownloadMod.IsEnabled)
                    .DisposeWith(d);

                ViewModel
                    .WhenAnyValue(vm => vm.CanRemove)
                    .ObserveOn(Dispatcher)
                    .BindTo(this, v => v.RemoveMod.IsEnabled)
                    .DisposeWith(d);

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
                    .OneWayBind(ViewModel, vm => vm.RibbonText, v => v.RibbonPanel.Visibility, ribbonText => string.IsNullOrWhiteSpace(ribbonText.Trim()) ? System.Windows.Visibility.Hidden : System.Windows.Visibility.Visible)
                    .DisposeWith(d);

                this
                    .OneWayBind(ViewModel, vm => vm.RibbonColor, v => v.RibbonPanel.Background, ribbonColor => Utilities.GetColorFromBridgepourRibbonColor(ribbonColor))
                    .DisposeWith(d);

                this
                    .OneWayBind(ViewModel, vm => vm.RibbonText, v => v.RibbonLabel.Text)
                    .DisposeWith(d);

                DownloadMod
                    .Events()
                    .Click
                    .Select(e => Unit.Default)
                    .InvokeCommand(ViewModel.DownloadModCommand)
                    .DisposeWith(d);

                RemoveMod
                    .Events()
                    .Click
                    .Select(e => Unit.Default)
                    .InvokeCommand(ViewModel.RemoveModCommand)
                    .DisposeWith(d);
            });
        }

        public void UpdateState()
        {
            ViewModel!.CheckIfExists();
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
