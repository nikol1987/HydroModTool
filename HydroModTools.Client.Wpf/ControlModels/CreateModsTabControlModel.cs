using HydroModTools.Client.Wpf.Enums;
using HydroModTools.Client.Wpf.State;
using ReactiveUI;
using System.Reactive;

namespace HydroModTools.Client.Wpf.ControlModels
{
    internal sealed class CreateModsTabControlModel : ReactiveObject
    {
        private readonly AppState _appState;

        public CreateModsTabControlModel(AppState appState)
        {
            _appState = appState;

            SetSelectedTabCommand = ReactiveCommand.Create<int>(SetSelectedTab);
        }

        public ReactiveCommand<int, Unit> SetSelectedTabCommand;
        private void SetSelectedTab(int tabIndex)
        {
            _appState.CurrentTab = AppTabs.PROJECTS + tabIndex;
        }
    }
}
