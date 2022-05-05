
using HydroModTools.Client.WinForms.Controls;

namespace HydroModTools.Winforms.Client.Views.ApplicationTabs
{
    partial class ModSegmentView
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
            this.modName = new System.Windows.Forms.Label();
            this.author = new System.Windows.Forms.Label();
            this.description = new HydroModTools.Client.WinForms.Controls.GrowLabel();
            this.downloadMod = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.removeMod = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // modName
            // 
            this.modName.AutoSize = true;
            this.modName.BackColor = System.Drawing.Color.Transparent;
            this.modName.Font = new System.Drawing.Font("Roboto", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.modName.Location = new System.Drawing.Point(10, 9);
            this.modName.MaximumSize = new System.Drawing.Size(500, 0);
            this.modName.Name = "modName";
            this.modName.Size = new System.Drawing.Size(144, 39);
            this.modName.TabIndex = 0;
            this.modName.Text = "Mod Name";
            this.modName.UseCompatibleTextRendering = true;
            // 
            // author
            // 
            this.author.AutoSize = true;
            this.author.BackColor = System.Drawing.Color.Transparent;
            this.author.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.author.Location = new System.Drawing.Point(10, 39);
            this.author.Name = "author";
            this.author.Size = new System.Drawing.Size(85, 25);
            this.author.TabIndex = 1;
            this.author.Text = "ResaloliPT";
            this.author.UseCompatibleTextRendering = true;
            // 
            // description
            // 
            this.description.AutoSize = true;
            this.description.BackColor = System.Drawing.Color.Transparent;
            this.description.Location = new System.Drawing.Point(10, 73);
            this.description.MaximumSize = new System.Drawing.Size(460, 70);
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(95, 15);
            this.description.TabIndex = 2;
            this.description.Text = "Mod Description";
            // 
            // downloadMod
            // 
            this.downloadMod.Location = new System.Drawing.Point(10, 143);
            this.downloadMod.Name = "downloadMod";
            this.downloadMod.Size = new System.Drawing.Size(90, 25);
            this.downloadMod.TabIndex = 3;
            this.downloadMod.Values.Text = "Download";
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.removeMod);
            this.kryptonPanel1.Controls.Add(this.description);
            this.kryptonPanel1.Controls.Add(this.author);
            this.kryptonPanel1.Controls.Add(this.modName);
            this.kryptonPanel1.Controls.Add(this.downloadMod);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(493, 177);
            this.kryptonPanel1.TabIndex = 4;
            // 
            // removeMod
            // 
            this.removeMod.Location = new System.Drawing.Point(106, 143);
            this.removeMod.Name = "removeMod";
            this.removeMod.Size = new System.Drawing.Size(90, 25);
            this.removeMod.TabIndex = 4;
            this.removeMod.Values.Text = "Remove";
            // 
            // ModSegmentView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonPanel1);
            this.Name = "ModSegmentView";
            this.Size = new System.Drawing.Size(493, 177);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label modName;
        private System.Windows.Forms.Label author;
        private GrowLabel description;
        private ComponentFactory.Krypton.Toolkit.KryptonButton downloadMod;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton removeMod;
    }
}
