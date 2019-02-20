namespace DbTextEditor.Forms.Dialogs
{
    partial class SaveFileDialogForm
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
            this.ToLocalRadioButton = new System.Windows.Forms.RadioButton();
            this.ToDbRadioButton = new System.Windows.Forms.RadioButton();
            this.ToLocalFileName = new System.Windows.Forms.TextBox();
            this.ToLocalChoosePathButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.ToDbFileName = new System.Windows.Forms.TextBox();
            this.SaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // ToLocalRadioButton
            // 
            this.ToLocalRadioButton.AutoSize = true;
            this.ToLocalRadioButton.Checked = true;
            this.ToLocalRadioButton.Location = new System.Drawing.Point(12, 12);
            this.ToLocalRadioButton.Name = "ToLocalRadioButton";
            this.ToLocalRadioButton.Size = new System.Drawing.Size(101, 21);
            this.ToLocalRadioButton.TabIndex = 0;
            this.ToLocalRadioButton.TabStop = true;
            this.ToLocalRadioButton.Text = "To local file";
            this.ToLocalRadioButton.UseVisualStyleBackColor = true;
            // 
            // ToDbRadioButton
            // 
            this.ToDbRadioButton.AutoSize = true;
            this.ToDbRadioButton.Location = new System.Drawing.Point(12, 67);
            this.ToDbRadioButton.Name = "ToDbRadioButton";
            this.ToDbRadioButton.Size = new System.Drawing.Size(109, 21);
            this.ToDbRadioButton.TabIndex = 1;
            this.ToDbRadioButton.Text = "To database";
            this.ToDbRadioButton.UseVisualStyleBackColor = true;
            // 
            // ToLocalFileName
            // 
            this.ToLocalFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ToLocalFileName.Location = new System.Drawing.Point(12, 39);
            this.ToLocalFileName.Name = "ToLocalFileName";
            this.ToLocalFileName.Size = new System.Drawing.Size(304, 22);
            this.ToLocalFileName.TabIndex = 2;
            // 
            // ToLocalChoosePathButton
            // 
            this.ToLocalChoosePathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ToLocalChoosePathButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToLocalChoosePathButton.Location = new System.Drawing.Point(322, 39);
            this.ToLocalChoosePathButton.Name = "ToLocalChoosePathButton";
            this.ToLocalChoosePathButton.Size = new System.Drawing.Size(44, 23);
            this.ToLocalChoosePathButton.TabIndex = 3;
            this.ToLocalChoosePathButton.Text = "...";
            this.ToLocalChoosePathButton.UseVisualStyleBackColor = true;
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SaveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveButton.Location = new System.Drawing.Point(12, 130);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(168, 31);
            this.SaveButton.TabIndex = 5;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelButton.Location = new System.Drawing.Point(192, 130);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(168, 31);
            this.CancelButton.TabIndex = 6;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // ToDbBox
            // 
            this.ToDbFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ToDbFileName.Location = new System.Drawing.Point(12, 94);
            this.ToDbFileName.Name = "ToDbFileName";
            this.ToDbFileName.Size = new System.Drawing.Size(354, 22);
            this.ToDbFileName.TabIndex = 7;
            // 
            // SaveDialog
            // 
            this.SaveDialog.FileName = "";
            // 
            // SaveFileDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 174);
            this.Controls.Add(this.ToDbFileName);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.ToLocalChoosePathButton);
            this.Controls.Add(this.ToLocalFileName);
            this.Controls.Add(this.ToDbRadioButton);
            this.Controls.Add(this.ToLocalRadioButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SaveFileDialogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Save file as";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton ToLocalRadioButton;
        private System.Windows.Forms.RadioButton ToDbRadioButton;
        private System.Windows.Forms.TextBox ToLocalFileName;
        private System.Windows.Forms.Button ToLocalChoosePathButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.TextBox ToDbFileName;
        private System.Windows.Forms.SaveFileDialog SaveDialog;
    }
}