
namespace HydroneerStager.WinForms.Views.ApplicationTabs.ProjectTabs
{
    partial class ProjectsView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectsView));
            this.projectListBox = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.progressBarLabel = new System.Windows.Forms.Label();
            this.buttonSpecAny1 = new ComponentFactory.Krypton.Toolkit.ButtonSpecAny();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.addProject = new System.Windows.Forms.ToolStripMenuItem();
            this.stageProject = new System.Windows.Forms.ToolStripMenuItem();
            this.packageProject = new System.Windows.Forms.ToolStripMenuItem();
            this.copyMod = new System.Windows.Forms.ToolStripMenuItem();
            this.launchGame = new System.Windows.Forms.ToolStripMenuItem();
            this.devExpress = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu = new ComponentFactory.Krypton.Toolkit.KryptonContextMenu();
            this.projectItemsTree = new HydroneerStager.WinForms.Controls.MSTreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // projectListBox
            // 
            this.projectListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.projectListBox.DisplayMember = "Value";
            this.projectListBox.Location = new System.Drawing.Point(0, 27);
            this.projectListBox.Name = "projectListBox";
            this.projectListBox.Size = new System.Drawing.Size(202, 357);
            this.projectListBox.TabIndex = 0;
            this.projectListBox.ValueMember = "Key";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progressBar.Location = new System.Drawing.Point(8, 389);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(115, 23);
            this.progressBar.TabIndex = 1;
            // 
            // progressBarLabel
            // 
            this.progressBarLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progressBarLabel.AutoSize = true;
            this.progressBarLabel.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.progressBarLabel.Location = new System.Drawing.Point(126, 390);
            this.progressBarLabel.Name = "progressBarLabel";
            this.progressBarLabel.Size = new System.Drawing.Size(0, 19);
            this.progressBarLabel.TabIndex = 2;
            // 
            // buttonSpecAny1
            // 
            this.buttonSpecAny1.UniqueName = "B0811260F94C4ADFDEA59EBAC93BBFA8";
            // 
            // menuStrip
            // 
            this.menuStrip.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refresh,
            this.addProject,
            this.stageProject,
            this.packageProject,
            this.copyMod,
            this.launchGame,
            this.devExpress});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.ShowItemToolTips = true;
            this.menuStrip.Size = new System.Drawing.Size(651, 24);
            this.menuStrip.TabIndex = 3;
            // 
            // refresh
            // 
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(58, 20);
            this.refresh.Text = "Refresh";
            this.refresh.ToolTipText = "Refresh config";
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
            this.stageProject.ToolTipText = "Stage Current Project";
            // 
            // packageProject
            // 
            this.packageProject.Name = "packageProject";
            this.packageProject.Size = new System.Drawing.Size(63, 20);
            this.packageProject.Text = "Package";
            this.packageProject.ToolTipText = "Package Current Project";
            // 
            // copyMod
            // 
            this.copyMod.Name = "copyMod";
            this.copyMod.Size = new System.Drawing.Size(75, 20);
            this.copyMod.Text = "Copy Mod";
            this.copyMod.ToolTipText = "Copy mod to game folder";
            // 
            // launchGame
            // 
            this.launchGame.Name = "launchGame";
            this.launchGame.Size = new System.Drawing.Size(92, 20);
            this.launchGame.Text = "Launch Game";
            this.launchGame.ToolTipText = "Launch Game (steam only)";
            // 
            // devExpress
            // 
            this.devExpress.Name = "devExpress";
            this.devExpress.Size = new System.Drawing.Size(81, 20);
            this.devExpress.Text = "Dev Express";
            this.devExpress.ToolTipText = "Stage & Package & Copy & Launch";
            // 
            // projectItemsTree
            // 
            this.projectItemsTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectItemsTree.Location = new System.Drawing.Point(0, 0);
            this.projectItemsTree.Name = "projectItemsTree";
            this.projectItemsTree.SelectedNodes = ((System.Collections.Generic.List<System.Windows.Forms.TreeNode>)(resources.GetObject("projectItemsTree.SelectedNodes")));
            this.projectItemsTree.ShowPlusMinus = false;
            this.projectItemsTree.Size = new System.Drawing.Size(443, 357);
            this.projectItemsTree.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.projectItemsTree);
            this.panel1.Location = new System.Drawing.Point(208, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(443, 357);
            this.panel1.TabIndex = 8;
            // 
            // ProjectsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.progressBarLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.projectListBox);
            this.Controls.Add(this.menuStrip);
            this.Name = "ProjectsView";
            this.Size = new System.Drawing.Size(651, 417);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonListBox projectListBox;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label progressBarLabel;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecAny buttonSpecAny1;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem addProject;
        private System.Windows.Forms.ToolStripMenuItem stageProject;
        private System.Windows.Forms.ToolStripMenuItem packageProject;
        private System.Windows.Forms.ToolStripMenuItem copyMod;
        public ComponentFactory.Krypton.Toolkit.KryptonContextMenu contextMenu;
        private Controls.MSTreeView projectItemsTree;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem refresh;
        private System.Windows.Forms.ToolStripMenuItem launchGame;
        private System.Windows.Forms.ToolStripMenuItem devExpress;
    }
}
