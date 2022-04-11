using System;
using System.Windows.Controls;
using Autofac;
using HandyControl.Controls;
using ReactiveUI;

namespace HydroModTools.Wpf.DI
{
    internal sealed class DiIWpfFactory : IWpfFactory
    {
        private readonly IContainer _services;

        public DiIWpfFactory(IContainer services)
        {
            _services = services;
        }

        public UserControl CreateUserControl(Type controlType)
        {
            var userControl = (UserControl)_services.Resolve(controlType);
            return userControl;
        }

        public ReactiveObject CreateViewModel(Type viewModelType)
        {
            var modelView = (ReactiveObject)_services.Resolve(viewModelType);
            return modelView;
        }

        public Window CreateWindow(Type windowType)
        {
            var window = (Window) _services.Resolve(windowType);
            return window;
        }
    }
}