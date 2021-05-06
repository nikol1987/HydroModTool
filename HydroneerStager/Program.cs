using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HydroneerStager
{
    static class Program
    {

        private static Timer _splashScreenTimer;

        private static Form _app = new App();
        private static Form _splashScreen = new SplashScreen();

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            await Store.InitAsync();

            _splashScreenTimer = new Timer();
            _splashScreenTimer.Interval = 1000;

            _splashScreenTimer.Tick += TimeoutTimer_Tick;
            _splashScreenTimer.Start();

            _splashScreen.ShowDialog();

            _app.Hide();
            Application.Run(_app);
            Application.ApplicationExit += (object sender, EventArgs e) => {
                Store.Save();
            };
        }

        private static void TimeoutTimer_Tick(object sender, EventArgs e)
        {
            _splashScreenTimer.Enabled = false;
            _splashScreenTimer.Stop();

            _app.Show();
            _splashScreen.Close();
        }
    }
}
