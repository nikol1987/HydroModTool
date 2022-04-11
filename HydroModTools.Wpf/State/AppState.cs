using HydroModTools.Wpf.Enums;
using ReactiveUI;

namespace HydroModTools.Wpf.State
{
    internal class AppState : ReactiveObject
    {
        private AppTabs _currentTab = AppTabs.INSTALL_MODS;
        public AppTabs CurrentTab
        {
            get => _currentTab;
            set
            {
                this.RaiseAndSetIfChanged(ref _currentTab, value);
            }
        }
    }
}
