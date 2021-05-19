using Autofac;
using HydroneerStager.DI;
using HydroneerStager.WinForms.Views;
using System;
using System.Windows.Forms;

namespace HydroneerStager
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AutoFac.BuildDependencies();

            Application.ApplicationExit += async (sender, e) =>
            {
                var configuration = AutoFac.Services.Resolve<Configuration>();

                await configuration.Save();
            };

            var appForm = FormFactory.Create<ApplicationView>();
            appForm.Hide();

            var spashForm = FormFactory.Create<SpashView>();
            spashForm.ShowDialog();

            Application.Run(appForm);
        }

        public static void ShowApp()
        {
            var appForm = FormFactory.Create<ApplicationView>();
            appForm.Show();
        }
    }
}
