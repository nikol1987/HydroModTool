using HandyControl.Controls;
using HydroModTools.Wpf.DI;
using HydroModTools.Wpf.ViewModels;
using ReactiveUI;
using System.Reactive.Linq;
using ReactiveMarbles.ObservableEvents;
using System;
using HydroModTools.Wpf.Controls;

namespace HydroModTools.Wpf.Views
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
