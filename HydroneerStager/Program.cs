using Autofac;
using HydroneerStager.DI;
using HydroneerStager.WinForms.Views;
using System;
using System.Windows.Forms;
//using System.Runtime.InteropServices;


namespace HydroneerStager
{
    static class Program
    {

        /*[DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();*/

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
