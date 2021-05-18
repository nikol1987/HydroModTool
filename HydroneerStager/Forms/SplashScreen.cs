using System.ComponentModel;
using System.Windows.Forms;

namespace HydroneerStager
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();

            loadingWorker.DoWork += async (object sender, DoWorkEventArgs e) =>
            {
                loadingWorker.ReportProgress(1, new LoadingWorkerStage("Loading Configuration"));

                await Store.GetInstance().InitAsync();
            };

            loadingWorker.ProgressChanged += (object sender, ProgressChangedEventArgs e) =>
            {
                loadingPhaseLabel.Text = ((LoadingWorkerStage)e.UserState).Phase;
            };
            loadingWorker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
            {
                loadingPhaseLabel.Text = "Starting App";

                splashTimer.Enabled = true;
                splashTimer.Start();
            };
            loadingWorker.WorkerReportsProgress = true;
            loadingWorker.WorkerSupportsCancellation = true;

            splashTimer.Tick += (object sender, System.EventArgs e) =>
            {
                splashTimer.Enabled = false;
                splashTimer.Stop();

                Program.ShowApp();
                Close();
            };

            loadingWorker.RunWorkerAsync();
        }

        internal class LoadingWorkerStage
        {
            public LoadingWorkerStage(string phase)
            {
                Phase = phase;
            }

            public string Phase { get; }
        }
    }
}