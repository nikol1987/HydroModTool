using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using HydroModTools.Client.Abstractions;
using HydroModTools.Wpf.DI;
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

        public override void RegisterClientTypes(Action<IEnumerable<Type>> configure)
        {
            var types = typeof(Assembly).Assembly.GetTypes()
                .Where(t => t.Name.EndsWith("View"));
            
            configure.Invoke(types);
        }

        public override void ConfigureServices(IContainer services)
        {
            var formFactory = new DiFormFactory(services);
            FormFactory.Use(formFactory);

            _appThread = new Thread(() =>
            {
                var app = new Application();
                app.ShutdownMode = ShutdownMode.OnExplicitShutdown;

                _splashView = FormFactory.Create<SplashView>();
                MainForm = FormFactory.Create<ApplicationView>();

                app.Run();
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
    }
}