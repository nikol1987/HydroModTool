﻿using HydroModTools.Contracts.Services;
using HydroModTools.WinForms.Data;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace HydroModTools.WinForms.Views
{
    public partial class SpashView : Form
    {
        public event Action TimeOutEvent; 

        public SpashView(IConfigurationService configurationService)
        {
            InitializeComponent();

            pictureBox2.Parent = this.pictureBox1;
            pictureBox2.BringToFront();

            label1.Parent = this.pictureBox1;
            label1.BringToFront();

            loadingLabel.Parent = this.pictureBox1;
            loadingLabel.BringToFront();

            loadingWorker.DoWork += async (object sender, DoWorkEventArgs e) =>
            {
                loadingWorker.ReportProgress(1, new LoadingWorkerStage("Loading Configuration"));

                var config = await configurationService.GetAsync();

                await ApplicationStore.RefreshStore(config);
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
