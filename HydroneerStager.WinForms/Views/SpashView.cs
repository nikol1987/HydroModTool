using HydroneerStager.WinForms.ViewModels;
using ReactiveUI;
using System.ComponentModel;
using System.Windows.Forms;

namespace HydroneerStager.WinForms.Views
{
    public partial class SpashView : Form, IViewFor<SpashViewModel>
    {
        public SpashView(SpashViewModel spashViewModel)
        {
            InitializeComponent();

            this.pictureBox2.Parent = this.pictureBox1;
            this.pictureBox2.BringToFront();

            this.label1.Parent = this.pictureBox1;
            this.label1.BringToFront();

            this.loadingLabel.Parent = this.pictureBox1;
            this.loadingLabel.BringToFront();

            loadingWorker.DoWork += async (object sender, DoWorkEventArgs e) => {
                loadingWorker.ReportProgress(1, new LoadingWorkerStage("Loading Configuration"));

                //await Store.GetInstance().InitAsync(); //TODO: Get Store DIed
            };

            loadingWorker.ProgressChanged += (object sender, ProgressChangedEventArgs e) => {
                loadingLabel.Text = ((LoadingWorkerStage)e.UserState).Phase;
            };
            loadingWorker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) => {
                loadingLabel.Text = "Starting App";

                splashTimer.Enabled = true;
                splashTimer.Start();
            };

            splashTimer.Tick += (object sender, System.EventArgs e) => {
                splashTimer.Enabled = false;
                splashTimer.Stop();

                spashViewModel.ShowAppCommand.Execute();
                Close();
            };

            ViewModel = spashViewModel;

            loadingWorker.RunWorkerAsync();
        }

        public SpashViewModel ViewModel { get; set; }

        object IViewFor.ViewModel { get => ViewModel; set => ViewModel = (SpashViewModel)value; }

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
