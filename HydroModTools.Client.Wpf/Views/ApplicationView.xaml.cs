using HandyControl.Controls;
using ReactiveUI;
using System.Reactive.Linq;
using ReactiveMarbles.ObservableEvents;
using System;
using HydroModTools.Client.Wpf.DI;
using HydroModTools.Client.Wpf.ViewModels;
using HydroModTools.Client.Wpf.Controls;

namespace HydroModTools.Client.Wpf.Views
{
    internal partial class ApplicationView : Window, IViewFor<ApplicationViewModel>
    {
        private bool wasInInstallTab = true;

        public ApplicationView()
        {
            ViewModel = WpfFactory.CreateViewModel<ApplicationViewModel>();

            InitializeComponent();

            var installModsTab = WpfFactory.CreateControl<InstallModsTabControl>();
            installModsTab.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            installModsTab.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            InstallModsTab.Content = installModsTab;

            this.WhenActivated((d) => {
                TabSelector
                    .Events()
                    .SelectionChanged
                    .Select(o => (TabControl)o.Source)
                    .Select(tabSelector => tabSelector.SelectedIndex)
                    .InvokeCommand(ViewModel.SetSelectedTabCommand);

                ViewModel
                    .ObservableForProperty(vm => vm.InInstallModsTab)
                    .Select(o => o.GetValue())
                    .Subscribe((inInstallTab) =>
                    {
                        if (inInstallTab && !wasInInstallTab)
                        {
                            Height = 720;
                            Width = 1006;
                            ResizeMode = System.Windows.ResizeMode.NoResize;
                            wasInInstallTab = true;
                        }
                        else
                        {
                            ResizeMode = System.Windows.ResizeMode.CanResizeWithGrip;
                            wasInInstallTab = false;
                        }
                    });

            });
        }

        object? IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (ApplicationViewModel)value!;
        }

        public ApplicationViewModel? ViewModel { get; set; }
    }
}
