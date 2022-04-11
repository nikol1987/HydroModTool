using DynamicData.Binding;
using HandyControl.Controls;
using HydroModTools.Common.Enums;
using HydroModTools.Wpf.ViewModels;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace HydroModTools.Wpf.Views
{
    public partial class SplashView : Window, IViewFor<SplashViewModel>
    {
        public SplashView()
        {
            ViewModel = new SplashViewModel();

            InitializeComponent();

            this.WhenActivated((d) =>
            {
                ViewModel
                    .ObservableForProperty(vm => vm.AppLoadStage)
                    .Select(o => o.GetValue())
                    .Select(state => state switch
                    {
                        AppLoadStage.Preload => "App Loading",
                        AppLoadStage.Load => "App Starting",
                        _ => throw new ArgumentOutOfRangeException("Invalid App State")
                    })
                    .BindTo(this, v => v.AppLoadingStateLabel.Text)
                    .DisposeWith(d);          
            });
        }

        object? IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (SplashViewModel)value!;
        }

        public SplashViewModel? ViewModel { get; set; }
    }
}
