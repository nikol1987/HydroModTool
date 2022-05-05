using HydroModTools.Client.Wpf.Enums;
using HydroModTools.Client.Wpf.State;
using ReactiveUI;
using System.Reactive;

namespace HydroModTools.Client.Wpf.ViewModels
{
    internal sealed class ApplicationViewModel : ReactiveObject
    {
        private readonly AppState _appState;

        public ApplicationViewModel(AppState appState)
        {
            _appState = appState;

            SetSelectedTabCommand = ReactiveCommand.Create<int>(SetSelectedTab);
        }

        public bool InInstallModsTab
        {
            get => _appState.CurrentTab == AppTabs.INSTALL_MODS;
        }

        public ReactiveCommand<int, Unit> SetSelectedTabCommand;
        private void SetSelectedTab(int tabIndex)
        {
            _appState.CurrentTab = (AppTabs)tabIndex;
            this.RaisePropertyChanged(nameof(InInstallModsTab));
        }
    }
}
