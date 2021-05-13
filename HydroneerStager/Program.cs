using System;
using System.Windows.Forms;

namespace HydroneerStager
{
    static class Program
    {
        private static Form _app = new App();
        private static Form _splashScreen = new SplashScreen();

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            _splashScreen.ShowDialog();

            _app.Hide();
            Application.Run(_app);
            Application.ApplicationExit += (object sender, EventArgs e) => {
                Store.GetInstance().Save();
            };
        }

        public static void ShowApp()
        {
            _app.Show();
        }
    }
}
