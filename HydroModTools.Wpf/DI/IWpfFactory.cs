using HandyControl.Controls;
using ReactiveUI;
using System;
using System.Windows.Controls;

namespace HydroModTools.Wpf.DI
{
    internal interface IWpfFactory
    {
        Window CreateWindow(Type windowType);

        ReactiveObject CreateViewModel(Type viewModelType);

        UserControl CreateUserControl(Type controlType);
    }
}
