using Autofac;
using HydroneerStager.DataAccess.Services;
using HydroneerStager.DI;
using HydroneerStager.WinForms.Views;
using System;
using System.Threading.Tasks;
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

            Application.ApplicationExit += (object sender, EventArgs e) => {
                Store.GetInstance().Save();
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
