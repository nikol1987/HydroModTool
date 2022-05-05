using DynamicData.Binding;
using HydroModTools.Common;
using HydroModTools.Common.Enums;
using ReactiveUI;
using System.Reactive.Linq;

namespace HydroModTools.Client.Wpf.ViewModels
{
    internal class SplashViewModel : ReactiveObject
    {
        public SplashViewModel()
        {
            DataInterop.Instance
                .WhenPropertyChanged(interop => interop.AppLoadStage)
                .Select(appLoadStage => appLoadStage.Value)
                .BindTo(this, model => model.AppLoadStage);
        }

        private AppLoadStage _appLoadStage = AppLoadStage.Preload;
        public AppLoadStage AppLoadStage
        {
            get => _appLoadStage;
            set => this.RaiseAndSetIfChanged(ref _appLoadStage, value);
        }
    }
}