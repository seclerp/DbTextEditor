namespace DbTextEditor.DIalogs
{
    partial class OpenFileDialogForm
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
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.FromFileBox = new System.Windows.Forms.TextBox();
            this.ChoosePathButton = new System.Windows.Forms.Button();
            this.FromDatabaseListView = new System.Windows.Forms.ListView();
            this.FileNameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.OpenButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(12, 12);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(116, 21);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.Text = "From local file";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(12, 67);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(124, 21);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "From database";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // FromFileBox
            // 
            this.FromFileBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FromFileBox.Location = new System.Drawing.Point(12, 39);
            this.FromFileBox.Name = "FromFileBox";
            this.FromFileBox.Size = new System.Drawing.Size(304, 22);
            this.FromFileBox.TabIndex = 2;
            // 
            // ChoosePathButton
            // 
            this.ChoosePathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ChoosePathButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChoosePathButton.Location = new System.Drawing.Point(322, 39);
            this.ChoosePathButton.Name = "ChoosePathButton";
            this.ChoosePathButton.Size = new System.Drawing.Size(44, 23);
            this.ChoosePathButton.TabIndex = 3;
            this.ChoosePathButton.Text = "...";
            this.ChoosePathButton.UseVisualStyleBackColor = true;
            // 
            // FromDatabaseListView
            // 
            this.FromDatabaseListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FromDatabaseListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FileNameColumn});
            this.FromDatabaseListView.Location = new System.Drawing.Point(12, 94);
            this.FromDatabaseListView.Name = "FromDatabaseListView";
            this.FromDatabaseListView.Size = new System.Drawing.Size(348, 231);
            this.FromDatabaseListView.TabIndex = 4;
            this.FromDatabaseListView.UseCompatibleStateImageBehavior = false;
            this.FromDatabaseListView.View = System.Windows.Forms.View.SmallIcon;
            // 
            // OpenButton
            // 
            this.OpenButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OpenButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OpenButton.Location = new System.Drawing.Point(12, 331);
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(168, 31);
            this.OpenButton.TabIndex = 5;
            this.OpenButton.Text = "Open";
            this.OpenButton.UseVisualStyleBackColor = true;
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelButton.Location = new System.Drawing.Point(192, 331);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(168, 31);
            this.CancelButton.TabIndex = 6;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // OpenFileDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 375);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OpenButton);
            this.Controls.Add(this.FromDatabaseListView);
            this.Controls.Add(this.ChoosePathButton);
            this.Controls.Add(this.FromFileBox);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OpenFileDialogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Open file";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.TextBox FromFileBox;
        private System.Windows.Forms.Button ChoosePathButton;
        private System.Windows.Forms.ListView FromDatabaseListView;
        private System.Windows.Forms.ColumnHeader FileNameColumn;
        private System.Windows.Forms.Button OpenButton;
        private System.Windows.Forms.Button CancelButton;
    }
}