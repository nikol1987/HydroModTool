using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using HydroModTools.Client.Abstractions;
using HydroModTools.Wpf.DI;
using HydroModTools.Wpf.State;
using HydroModTools.Wpf.Views;

namespace HydroModTools.Wpf
{
    public class WpfClient : ClientBase<Window>
    {
        private Thread? _appThread;
        private Window? _splashView;

        private bool _slashVisible = false;
        public override Task ToggleSplash(bool show)
        {
            if (!show)
            {
                if (!_slashVisible)
                {
                    throw new InvalidOperationException("Splash already closed");
                }

                _slashVisible = false;

                _splashView.Dispatcher.Invoke(() =>
                {
                    _splashView.Close();
                });
                return Task.CompletedTask;
            }

            if (_slashVisible)
            {
                throw new InvalidOperationException("Splash already open");
            }

            _splashView.Dispatcher.Invoke(() =>
            {
                _slashVisible = true;
                _splashView.Show();
            });

            return Task.CompletedTask;
        }

        public override Task RunClient()
        {
            var clientThread = new Thread(() =>
            {
                MainForm!.Dispatcher.Invoke(() =>
                {
                    MainForm.ShowDialog();
                });
            })
            {
                IsBackground = false,
                Priority = ThreadPriority.Normal
            };
            clientThread.SetApartmentState(ApartmentState.STA);

            clientThread.Start();
            return Task.Run(clientThread.Join);
        }

        public override void RegisterClientTypes(ContainerBuilder services)
        {
            var types = typeof(Assembly).Assembly.GetTypes();

            var selectedTypes = types
                .Where(t => t.Name.EndsWith("View") || t.Name.EndsWith("Control") || t.Name.EndsWith("ViewModel"))
                .ToList();

            services
                .RegisterTypes(selectedTypes.ToArray())
                .InstancePerDependency();

            services
                .RegisterType<AppState>()
                .SingleInstance();
        }

        public override void ConfigureServices(IContainer services)
        {
            var formFactory = new DiIWpfFactory(services);
            WpfFactory.Use(formFactory);

            _appThread = new Thread(() =>
            {
                var app = new HMTApp
                {
                    ShutdownMode = ShutdownMode.OnExplicitShutdown
                };

                _splashView = WpfFactory.CreateWindow<SplashView>();
                MainForm = WpfFactory.CreateWindow<ApplicationView>();

                app.StartApp();
            })
            {
                IsBackground = true,
                Priority = ThreadPriority.Normal
            };
            _appThread.SetApartmentState(ApartmentState.STA);
            _appThread.Start();

            while (MainForm == null)
            {
                Thread.Yield();
            }
        }

        private class HMTApp : Application
        {

            private bool _contentLoaded;

            public void InitializeComponent()
            {
                if (_contentLoaded)
                {
                    return;
                }
                _contentLoaded = true;

                var resourceLocater = new Uri("/HydroModTools.Wpf;component/app.xaml", System.UriKind.Relative);

                LoadComponent(this, resourceLocater);
            }

            [STAThread()]
            public void StartApp()
            {
                InitializeComponent();
                Run();
            }
        }
    }
}