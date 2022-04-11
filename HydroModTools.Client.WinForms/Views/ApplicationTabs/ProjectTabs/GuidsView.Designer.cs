
namespace HydroModTools.Winforms.Client.Views.ApplicationTabs.ProjectTabs
{
    partial class GuidsView
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
            this.addGuid = new System.Windows.Forms.ToolStripMenuItem();
            this.removeGuid = new System.Windows.Forms.ToolStripMenuItem();
            this.saveGuids = new System.Windows.Forms.ToolStripMenuItem();
            this.guidsDataGrid = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guidsDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addGuid,
            this.removeGuid,
            this.saveGuids});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(651, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // addGuid
            // 
            this.addGuid.Name = "addGuid";
            this.addGuid.Size = new System.Drawing.Size(71, 20);
            this.addGuid.Text = "Add GUID";
            this.addGuid.ToolTipText = "Add a new guid to the grid";
            // 
            // removeGuid
            // 
            this.removeGuid.Name = "removeGuid";
            this.removeGuid.Size = new System.Drawing.Size(92, 20);
            this.removeGuid.Text = "Remove GUID";
            this.removeGuid.ToolTipText = "Remove the selected GUID";
            // 
            // saveGuids
            // 
            this.saveGuids.Name = "saveGuids";
            this.saveGuids.Size = new System.Drawing.Size(78, 20);
            this.saveGuids.Text = "Save GUIDs";
            this.saveGuids.ToolTipText = "Saves current GUIDs";
            // 
            // guidsDataGrid
            // 
            this.guidsDataGrid.AllowUserToAddRows = false;
            this.guidsDataGrid.AllowUserToDeleteRows = false;
            this.guidsDataGrid.AllowUserToResizeRows = false;
            this.guidsDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guidsDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.guidsDataGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.guidsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.guidsDataGrid.Location = new System.Drawing.Point(0, 27);
            this.guidsDataGrid.MultiSelect = false;
            this.guidsDataGrid.Name = "guidsDataGrid";
            this.guidsDataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.guidsDataGrid.RowTemplate.Height = 25;
            this.guidsDataGrid.Size = new System.Drawing.Size(651, 390);
            this.guidsDataGrid.TabIndex = 1;
            // 
            // GuidsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.guidsDataGrid);
            this.Controls.Add(this.menuStrip1);
            this.Name = "GuidsView";
            this.Size = new System.Drawing.Size(651, 417);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guidsDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addGuid;
        private System.Windows.Forms.ToolStripMenuItem removeGuid;
        private System.Windows.Forms.ToolStripMenuItem saveGuids;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView guidsDataGrid;
    }
}
