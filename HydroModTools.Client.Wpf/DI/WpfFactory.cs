using HandyControl.Controls;
using ReactiveUI;
using System;
using System.Windows.Controls;

namespace HydroModTools.Client.Wpf.DI
{
    internal static class WpfFactory
    {
        private static IWpfFactory? _factory;

        public static void Use(IWpfFactory factory)
        {
            if (_factory != null)
            {
                throw new InvalidOperationException(@"Form factory has been already set up.");
            }

            _factory = factory;
        }

        public static T CreateWindow<T>() where T : Window
        {
            if (_factory == null)
            {
                throw new InvalidOperationException(@"Form factory has not been set up. Call the 'Use' method to inject an IFormFactory instance.");
            }

            return (T)_factory.CreateWindow(typeof(T));
        }

        public static T CreateControl<T>() where T : UserControl
        {
            if (_factory == null)
            {
                throw new InvalidOperationException(@"Form factory has not been set up. Call the 'Use' method to inject an IFormFactory instance.");
            }

            return (T)_factory.CreateUserControl(typeof(T));
        }

        public static T CreateViewModel<T>() where T : ReactiveObject
        {
            if (_factory == null)
            {
                throw new InvalidOperationException(@"Form factory has not been set up. Call the 'Use' method to inject an IFormFactory instance.");
            }

            return (T)_factory.CreateViewModel(typeof(T));
        }
    }
}
