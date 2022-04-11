using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;
using HydroModTools.Client.Abstractions;
using HydroModTools.WinForms.DI;
using HydroModTools.WinForms.Views;

namespace HydroModTools.WinForms
{
    public class WinFormsClient : ClientBase<ApplicationView>
    {
        public override Task RunClient()
        {
            var clientThread = new Thread(() =>
            {
                var applicationView = FormFactory.Create<ApplicationView>();
                Application.Run(applicationView);
            })
            {
                IsBackground = false,
                Priority = ThreadPriority.Normal
            };
            clientThread.SetApartmentState(ApartmentState.STA);

            clientThread.Start();

            return Task.Run(clientThread.Join);
        }


        public override Task ToggleSplash(bool show)
        {
            if (!show)
            {
                Application.Exit();
                return Task.CompletedTask;
            }

            var clientTask = new Task(() =>
            {                
                var splashForm = FormFactory.Create<SplashView>();
                Application.Run(splashForm);
            });

            clientTask.Start();

            return clientTask;
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
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }
    }
}