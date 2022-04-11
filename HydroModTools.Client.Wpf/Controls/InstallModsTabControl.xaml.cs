using ReactiveUI;
using System.Windows.Controls;
using System.Reactive.Linq;
using System;
using System.Linq;
using HydroModTools.Client.Wpf.ControlModels;
using HydroModTools.Client.Wpf.DI;
using System.Reactive;
using System.Reactive.Disposables;

namespace HydroModTools.Client.Wpf.Controls
{
    internal partial class InstallModsTabControl : UserControl, IViewFor<InstallModsTabControlModel>
    {
        public InstallModsTabControl()
        {
            ViewModel = WpfFactory.CreateViewModel<InstallModsTabControlModel>();

            InitializeComponent();

            this.WhenActivated(d => {
                RefreshBtn
                    .Events()
                    .Click
                    .Select(_ => Unit.Default)
                    .InvokeCommand(ViewModel!.RefreshCommand)
                    .DisposeWith(d);

                ClearModsBtn
                    .Events()
                    .Click
                    .Select(_ => Unit.Default)
                    .InvokeCommand(ViewModel!.ClearModsCommand)
                    .DisposeWith(d);

                OpenFolderBtn
                    .Events()
                    .Click
                    .Select(_ => Unit.Default)
                    .InvokeCommand(ViewModel!.OpenModsFolderCommand)
                    .DisposeWith(d);

                ViewModel
                    .WhenAnyValue(vm => vm.SecondsTillRefresh)
                    .ObserveOn(Dispatcher)
                    .Select(seconds => seconds > 0 ? $"Refresh ({seconds})" : "Refresh")
                    .BindTo(this, v => v.RefreshBtn.Header)
                    .DisposeWith(d);

                ViewModel
                    .WhenAnyValue(vm => vm.SecondsTillRefresh)
                    .ObserveOn(Dispatcher)
                    .Select(seconds => seconds == 0)
                    .BindTo(this, v => v.RefreshBtn.IsEnabled)
                    .DisposeWith(d);

                ViewModel
                    .WhenAnyValue(vm => vm.ModList)
                    .ObserveOn(Dispatcher)
                    .Subscribe(mods => {
                        var modSegments = mods
                            .Select(mod => {
                                var modSegment = WpfFactory.CreateControl<ModSegmentControl>();
                                modSegment.SetModDetails(mod);

                                return modSegment;
                            });

                        ModListPanel.Dispatcher.Invoke(() =>
                        {
                            ModListPanel.Children.Clear();

                            foreach (var modSegment in modSegments)
                            {
                                ModListPanel.Children.Add(modSegment);
                            }
                        });
                    })
                    .DisposeWith(d);
            });
        }

        object? IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (InstallModsTabControlModel)value!;
        }

        public InstallModsTabControlModel? ViewModel { get; set; }
    }
}
