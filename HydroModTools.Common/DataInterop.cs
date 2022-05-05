using HydroModTools.Common.Enums;
using ReactiveUI;

namespace HydroModTools.Common
{
    public class DataInterop : ReactiveObject
    {
        public static DataInterop Instance = new DataInterop();
        private DataInterop(){}
        
        
        private AppLoadStage _appLoadStage = AppLoadStage.Preload;
        public AppLoadStage AppLoadStage
        {
            get => _appLoadStage;
            set => this.RaiseAndSetIfChanged(ref _appLoadStage, value);
        }
    }
}