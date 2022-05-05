
namespace HydroModTools.Winforms.Client.Views.ApplicationTabs
{
    partial class ModInstallerTabView
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.refreshMods = new System.Windows.Forms.ToolStripMenuItem();
            this.clearMods = new System.Windows.Forms.ToolStripMenuItem();
            this.openModFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.modsContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshMods,
            this.clearMods,
            this.openModFolder});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.ShowItemToolTips = true;
            this.menuStrip1.Size = new System.Drawing.Size(651, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // refreshMods
            // 
            this.refreshMods.Name = "refreshMods";
            this.refreshMods.Size = new System.Drawing.Size(58, 20);
            this.refreshMods.Text = "Refresh";
            this.refreshMods.ToolTipText = "Refresh mod list";
            // 
            // clearMods
            // 
            this.clearMods.Name = "clearMods";
            this.clearMods.Size = new System.Drawing.Size(79, 20);
            this.clearMods.Text = "Clear mods";
            this.clearMods.ToolTipText = "Removes all mods for a clean install";
            // 
            // openModFolder
            // 
            this.openModFolder.Name = "openModFolder";
            this.openModFolder.Size = new System.Drawing.Size(117, 20);
            this.openModFolder.Text = "Open Mods Folder";
            // 
            // modsContainer
            // 
            this.modsContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modsContainer.AutoScroll = true;
            this.modsContainer.Location = new System.Drawing.Point(0, 27);
            this.modsContainer.Name = "modsContainer";
            this.modsContainer.Size = new System.Drawing.Size(651, 390);
            this.modsContainer.TabIndex = 1;
            // 
            // ModInstallerTabView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.modsContainer);
            this.Controls.Add(this.menuStrip1);
            this.Name = "ModInstallerTabView";
            this.Size = new System.Drawing.Size(651, 417);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem refreshMods;
        private System.Windows.Forms.FlowLayoutPanel modsContainer;
        private System.Windows.Forms.ToolStripMenuItem clearMods;
        private System.Windows.Forms.ToolStripMenuItem openModFolder;
    }
}
