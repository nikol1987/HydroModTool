
namespace HydroneerStager
{
    partial class App
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.projectsTab = new System.Windows.Forms.TabPage();
            this.guidsTab = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.projectsTab);
            this.tabControl1.Controls.Add(this.guidsTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(855, 521);
            this.tabControl1.TabIndex = 0;
            // 
            // projectsTab
            // 
            this.projectsTab.Location = new System.Drawing.Point(4, 24);
            this.projectsTab.Name = "projectsTab";
            this.projectsTab.Padding = new System.Windows.Forms.Padding(3);
            this.projectsTab.Size = new System.Drawing.Size(847, 493);
            this.projectsTab.TabIndex = 0;
            this.projectsTab.Text = "Projects";
            this.projectsTab.UseVisualStyleBackColor = true;
            // 
            // guidsTab
            // 
            this.guidsTab.Location = new System.Drawing.Point(4, 24);
            this.guidsTab.Name = "guidsTab";
            this.guidsTab.Padding = new System.Windows.Forms.Padding(3);
            this.guidsTab.Size = new System.Drawing.Size(847, 493);
            this.guidsTab.TabIndex = 1;
            this.guidsTab.Text = "GUIDs";
            this.guidsTab.UseVisualStyleBackColor = true;
            // 
            // App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 521);
            this.Controls.Add(this.tabControl1);
            this.MaximumSize = new System.Drawing.Size(1140, 770);
            this.MinimumSize = new System.Drawing.Size(590, 560);
            this.Name = "App";
            this.Text = "Hydroneer Asset Stager";
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage projectsTab;
        private System.Windows.Forms.TabPage guidsTab;
    }
}