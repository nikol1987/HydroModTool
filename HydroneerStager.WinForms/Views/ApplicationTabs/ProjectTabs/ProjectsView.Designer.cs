
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSpecAny1 = new ComponentFactory.Krypton.Toolkit.ButtonSpecAny();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.addProject = new System.Windows.Forms.ToolStripMenuItem();
            this.stageProject = new System.Windows.Forms.ToolStripMenuItem();
            this.packageProject = new System.Windows.Forms.ToolStripMenuItem();
            this.copyMod = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
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
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progressBar1.Location = new System.Drawing.Point(8, 389);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(115, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(126, 390);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Doing Something";
            // 
            // buttonSpecAny1
            // 
            this.buttonSpecAny1.UniqueName = "B0811260F94C4ADFDEA59EBAC93BBFA8";
            // 
            // menuStrip
            // 
            this.menuStrip.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addProject,
            this.stageProject,
            this.packageProject,
            this.copyMod});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(651, 24);
            this.menuStrip.TabIndex = 3;
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
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(383, 389);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "label2";
            // 
            // projectItemsTree
            // 
            this.projectItemsTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectItemsTree.Location = new System.Drawing.Point(0, 0);
            this.projectItemsTree.Name = "projectItemsTree";
            this.projectItemsTree.SelectedNodes = ((System.Collections.Generic.List<System.Windows.Forms.TreeNode>)(resources.GetObject("projectItemsTree.SelectedNodes")));
            this.projectItemsTree.Size = new System.Drawing.Size(443, 360);
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
            this.panel1.Size = new System.Drawing.Size(443, 360);
            this.panel1.TabIndex = 8;
            // 
            // ProjectsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
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
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecAny buttonSpecAny1;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem addProject;
        private System.Windows.Forms.ToolStripMenuItem stageProject;
        private System.Windows.Forms.ToolStripMenuItem packageProject;
        private System.Windows.Forms.ToolStripMenuItem copyMod;
        private System.Windows.Forms.Label label2;
        public ComponentFactory.Krypton.Toolkit.KryptonContextMenu contextMenu;
        private Controls.MSTreeView projectItemsTree;
        private System.Windows.Forms.Panel panel1;
    }
}
