
using System;

namespace HydroneerStager
{
    partial class ProjectsPage
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private System.Windows.Forms.ListBox projectListBox;
        private System.Windows.Forms.MenuStrip projectSettings;
        private System.Windows.Forms.ToolStripMenuItem addProject;
        private System.Windows.Forms.ToolStripMenuItem stageProject;
        private System.Windows.Forms.TreeView projectItemsView;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label progressLabel;
        private System.Windows.Forms.ProgressBar stageProgressbar;
        private System.ComponentModel.BackgroundWorker stagerWorker;
        private System.Windows.Forms.ToolStripMenuItem refreshPage;

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.projectListBox = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.projectSettings = new System.Windows.Forms.MenuStrip();
            this.addProject = new System.Windows.Forms.ToolStripMenuItem();
            this.stageProject = new System.Windows.Forms.ToolStripMenuItem();
            this.pakageProject = new System.Windows.Forms.ToolStripMenuItem();
            this.copyMod = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshPage = new System.Windows.Forms.ToolStripMenuItem();
            this.projectItemsView = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressLabel = new System.Windows.Forms.Label();
            this.stageProgressbar = new System.Windows.Forms.ProgressBar();
            this.stagerWorker = new System.ComponentModel.BackgroundWorker();
            this.packagerWorker = new System.ComponentModel.BackgroundWorker();
            this.projectSettings.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // projectListBox
            // 
            this.projectListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.projectListBox.FormattingEnabled = true;
            this.projectListBox.IntegralHeight = false;
            this.projectListBox.ItemHeight = 15;
            this.projectListBox.Location = new System.Drawing.Point(0, 27);
            this.projectListBox.Name = "projectListBox";
            this.projectListBox.Size = new System.Drawing.Size(184, 477);
            this.projectListBox.TabIndex = 1;
            this.projectListBox.SelectedIndexChanged += new System.EventHandler(this.projectListBox_Change);
            this.projectListBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.projectListBox_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.MinimumSize = new System.Drawing.Size(100, 0);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(100, 4);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // projectSettings
            // 
            this.projectSettings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addProject,
            this.stageProject,
            this.pakageProject,
            this.copyMod,
            this.refreshPage});
            this.projectSettings.Location = new System.Drawing.Point(0, 0);
            this.projectSettings.Name = "projectSettings";
            this.projectSettings.Size = new System.Drawing.Size(590, 24);
            this.projectSettings.TabIndex = 2;
            this.projectSettings.Text = "Add Project";
            // 
            // addProject
            // 
            this.addProject.Name = "addProject";
            this.addProject.Size = new System.Drawing.Size(81, 20);
            this.addProject.Text = "Add Project";
            this.addProject.ToolTipText = "Add a new Project";
            // 
            // stageProject
            // 
            this.stageProject.Name = "stageProject";
            this.stageProject.Size = new System.Drawing.Size(48, 20);
            this.stageProject.Text = "Stage";
            this.stageProject.ToolTipText = "Stages selected Project";
            // 
            // pakageProject
            // 
            this.pakageProject.Name = "pakageProject";
            this.pakageProject.Size = new System.Drawing.Size(63, 20);
            this.pakageProject.Text = "Package";
            this.pakageProject.ToolTipText = "Creates a .pak";
            // 
            // copyMod
            // 
            this.copyMod.Name = "copyMod";
            this.copyMod.Size = new System.Drawing.Size(75, 20);
            this.copyMod.Text = "Copy mod";
            this.copyMod.ToolTipText = "Copy mod pak to game folder";
            // 
            // refreshPage
            // 
            this.refreshPage.Name = "refreshPage";
            this.refreshPage.Size = new System.Drawing.Size(58, 20);
            this.refreshPage.Text = "Refresh";
            this.refreshPage.ToolTipText = "Refreshes this page";
            // 
            // projectItemsView
            // 
            this.projectItemsView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.projectItemsView.ContextMenuStrip = this.contextMenuStrip1;
            this.projectItemsView.Location = new System.Drawing.Point(190, 27);
            this.projectItemsView.Name = "projectItemsView";
            this.projectItemsView.ShowPlusMinus = false;
            this.projectItemsView.Size = new System.Drawing.Size(400, 477);
            this.projectItemsView.TabIndex = 3;
            this.projectItemsView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.projectItemsView_MouseUp);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.progressLabel);
            this.panel1.Controls.Add(this.stageProgressbar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 510);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(590, 50);
            this.panel1.TabIndex = 4;
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Location = new System.Drawing.Point(190, 18);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(0, 15);
            this.progressLabel.TabIndex = 1;
            // 
            // stageProgressbar
            // 
            this.stageProgressbar.Location = new System.Drawing.Point(0, 9);
            this.stageProgressbar.Name = "stageProgressbar";
            this.stageProgressbar.Size = new System.Drawing.Size(184, 34);
            this.stageProgressbar.Step = 1;
            this.stageProgressbar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.stageProgressbar.TabIndex = 0;
            // 
            // ProjectsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.projectItemsView);
            this.Controls.Add(this.projectListBox);
            this.Controls.Add(this.projectSettings);
            this.Name = "ProjectsPage";
            this.Size = new System.Drawing.Size(590, 560);
            this.projectSettings.ResumeLayout(false);
            this.projectSettings.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem pakageProject;
        private System.ComponentModel.BackgroundWorker packagerWorker;
        private System.Windows.Forms.ToolStripMenuItem copyMod;
    }
}
