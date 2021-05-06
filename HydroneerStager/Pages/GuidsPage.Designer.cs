
namespace HydroneerStager.Pages
{
    partial class GuidsPage
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
            this.guidsGrid = new System.Windows.Forms.DataGridView();
            this.saveGuids = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guidsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addGuid,
            this.saveGuids});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(963, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // addGuid
            // 
            this.addGuid.Name = "addGuid";
            this.addGuid.Size = new System.Drawing.Size(41, 20);
            this.addGuid.Text = "Add";
            this.addGuid.ToolTipText = "Add new Guid";
            // 
            // guidsGrid
            // 
            this.guidsGrid.AllowUserToAddRows = false;
            this.guidsGrid.AllowUserToResizeColumns = false;
            this.guidsGrid.AllowUserToResizeRows = false;
            this.guidsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guidsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.guidsGrid.Location = new System.Drawing.Point(0, 24);
            this.guidsGrid.Name = "guidsGrid";
            this.guidsGrid.RowTemplate.Height = 25;
            this.guidsGrid.Size = new System.Drawing.Size(963, 466);
            this.guidsGrid.TabIndex = 1;
            this.guidsGrid.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.guidsGrid_UserDeletingRow);
            // 
            // saveGuids
            // 
            this.saveGuids.Name = "saveGuids";
            this.saveGuids.Size = new System.Drawing.Size(43, 20);
            this.saveGuids.Text = "Save";
            this.saveGuids.ToolTipText = "Save all guids";
            // 
            // GuidsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.guidsGrid);
            this.Controls.Add(this.menuStrip1);
            this.Name = "GuidsPage";
            this.Size = new System.Drawing.Size(963, 490);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guidsGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addGuid;
        private System.Windows.Forms.DataGridView guidsGrid;
        private System.Windows.Forms.ToolStripMenuItem saveGuids;
    }
}
