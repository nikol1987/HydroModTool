using HydroneerStager.Contracts.Models.WinformModels;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace HydroneerStager.WinForms.Data
{
    public sealed class ApplicationStore : ReactiveObject
    {
        public event Func<Task<AppStateModel>> LoadConfig;

        public event Action<AppStateModel> SaveConfig;

        private AppStateModel _appStateModel;
        public AppStateModel AppState
        {
            get => _appStateModel; set
            {
                this.RaiseAndSetIfChanged(ref _appStateModel, value);
            }
        }

        public ApplicationStore(AppStateModel appStateModel)
        {
            _appStateModel = appStateModel;

            SetStateEvents();
        }

        private void StateChange()
        {
            SaveConfig.Invoke(_appStateModel);
        }

        public async Task ReloadState()
        {
            ClearStateEvents();
            AppState = await LoadConfig.Invoke();
            SetStateEvents();
        }

        private void ClearStateEvents()
        {
            _appStateModel.PropertyChanged -= AppStateChanged;
        }

        private void SetStateEvents()
        {
            _appStateModel.PropertyChanged += AppStateChanged;
        }

        private void AppStateChanged(object sender, PropertyChangedEventArgs e)
        {
            StateChange();
        }
    }
}
