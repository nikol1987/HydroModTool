using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace HydroModTools.WinForms.Views
{
    public partial class SpashView : Form
    {
        public event Action TimeOutEvent; 

        public SpashView()
        {
            InitializeComponent();

            this.pictureBox2.Parent = this.pictureBox1;
            this.pictureBox2.BringToFront();

            this.label1.Parent = this.pictureBox1;
            this.label1.BringToFront();

            this.loadingLabel.Parent = this.pictureBox1;
            this.loadingLabel.BringToFront();

            loadingWorker.DoWork += async (object sender, DoWorkEventArgs e) =>
            {
                loadingWorker.ReportProgress(1, new LoadingWorkerStage("Loading Configuration"));
            };

            loadingWorker.ProgressChanged += (sender, e) =>
            {
                loadingLabel.Text = ((LoadingWorkerStage)e.UserState).Phase;
            };
            loadingWorker.RunWorkerCompleted += (sender, e) =>
            {
                loadingLabel.Text = "Starting App";

                splashTimer.Enabled = true;
                splashTimer.Start();
            };

            splashTimer.Tick += (sender, e) =>
            {
                splashTimer.Enabled = false;
                splashTimer.Stop();

                TimeOutEvent.Invoke();
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
