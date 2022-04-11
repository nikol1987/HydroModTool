
namespace HydroModTools.Winforms.Views.ApplicationTabs.ProjectTabs
{
    partial class AddProjectView
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
            this.label1 = new System.Windows.Forms.Label();
            this.projectNameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cookedAssetsDirTextBox = new System.Windows.Forms.TextBox();
            this.selectCookedDirBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.outputPathDirTextBox = new System.Windows.Forms.TextBox();
            this.outputPathDirBtn = new System.Windows.Forms.Button();
            this.submit = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.projectIndexTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Project Name";
            // 
            // projectNameTextBox
            // 
            this.projectNameTextBox.Location = new System.Drawing.Point(38, 43);
            this.projectNameTextBox.Name = "projectNameTextBox";
            this.projectNameTextBox.PlaceholderText = "Project Name";
            this.projectNameTextBox.Size = new System.Drawing.Size(199, 23);
            this.projectNameTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Cooked Assets";
            // 
            // cookedAssetsDirTextBox
            // 
            this.cookedAssetsDirTextBox.Location = new System.Drawing.Point(38, 98);
            this.cookedAssetsDirTextBox.Name = "cookedAssetsDirTextBox";
            this.cookedAssetsDirTextBox.PlaceholderText = "Cooked Assets Dir";
            this.cookedAssetsDirTextBox.Size = new System.Drawing.Size(401, 23);
            this.cookedAssetsDirTextBox.TabIndex = 1;
            // 
            // selectCookedDirBtn
            // 
            this.selectCookedDirBtn.Location = new System.Drawing.Point(445, 98);
            this.selectCookedDirBtn.Name = "selectCookedDirBtn";
            this.selectCookedDirBtn.Size = new System.Drawing.Size(24, 23);
            this.selectCookedDirBtn.TabIndex = 2;
            this.selectCookedDirBtn.Text = "...";
            this.selectCookedDirBtn.UseVisualStyleBackColor = true;
            this.selectCookedDirBtn.Click += new System.EventHandler(this.selectCookedDirBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Output Path";
            // 
            // outputPathDirTextBox
            // 
            this.outputPathDirTextBox.Location = new System.Drawing.Point(38, 149);
            this.outputPathDirTextBox.Name = "outputPathDirTextBox";
            this.outputPathDirTextBox.PlaceholderText = "Output path dir";
            this.outputPathDirTextBox.Size = new System.Drawing.Size(401, 23);
            this.outputPathDirTextBox.TabIndex = 1;
            // 
            // outputPathDirBtn
            // 
            this.outputPathDirBtn.Location = new System.Drawing.Point(445, 149);
            this.outputPathDirBtn.Name = "outputPathDirBtn";
            this.outputPathDirBtn.Size = new System.Drawing.Size(24, 23);
            this.outputPathDirBtn.TabIndex = 2;
            this.outputPathDirBtn.Text = "...";
            this.outputPathDirBtn.UseVisualStyleBackColor = true;
            this.outputPathDirBtn.Click += new System.EventHandler(this.outputPathDirBtn_Click);
            // 
            // submit
            // 
            this.submit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.submit.Location = new System.Drawing.Point(38, 204);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(75, 23);
            this.submit.TabIndex = 3;
            this.submit.Text = "Done";
            this.submit.UseVisualStyleBackColor = true;
            this.submit.Click += new System.EventHandler(this.submit_Click);
            // 
            // cancel
            // 
            this.cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancel.Location = new System.Drawing.Point(394, 204);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 3;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // projectIndexTextBox
            // 
            this.projectIndexTextBox.Location = new System.Drawing.Point(243, 43);
            this.projectIndexTextBox.Name = "projectIndexTextBox";
            this.projectIndexTextBox.PlaceholderText = "500";
            this.projectIndexTextBox.Size = new System.Drawing.Size(196, 23);
            this.projectIndexTextBox.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(243, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "Mod Index";
            // 
            // AddProjectView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.projectIndexTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.submit);
            this.Controls.Add(this.outputPathDirBtn);
            this.Controls.Add(this.selectCookedDirBtn);
            this.Controls.Add(this.outputPathDirTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cookedAssetsDirTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.projectNameTextBox);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(510, 250);
            this.MinimumSize = new System.Drawing.Size(510, 250);
            this.Name = "AddProjectView";
            this.Size = new System.Drawing.Size(510, 250);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox projectNameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox cookedAssetsDirTextBox;
        private System.Windows.Forms.Button selectCookedDirBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox outputPathDirTextBox;
        private System.Windows.Forms.Button outputPathDirBtn;
        private System.Windows.Forms.Button submit;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.TextBox projectIndexTextBox;
        private System.Windows.Forms.Label label4;
    }
}
