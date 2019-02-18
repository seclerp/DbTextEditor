namespace DbTextEditor.Forms
{
    partial class DatabaseViewForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Row1", "file-icon.png");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Row2", "file-icon.png");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Row3", "file-icon.png");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseViewForm));
            this.FilesListView = new System.Windows.Forms.ListView();
            this.FileNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ListViewImageList = new System.Windows.Forms.ImageList(this.components);
            this.FilterBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FilesListView
            // 
            this.FilesListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FilesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FileNameHeader});
            this.FilesListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.FilesListView.Location = new System.Drawing.Point(12, 50);
            this.FilesListView.Name = "FilesListView";
            this.FilesListView.Size = new System.Drawing.Size(249, 240);
            this.FilesListView.SmallImageList = this.ListViewImageList;
            this.FilesListView.TabIndex = 0;
            this.FilesListView.UseCompatibleStateImageBehavior = false;
            this.FilesListView.View = System.Windows.Forms.View.List;
            // 
            // FileNameHeader
            // 
            this.FileNameHeader.Width = 212;
            // 
            // ListViewImageList
            // 
            this.ListViewImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ListViewImageList.ImageStream")));
            this.ListViewImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ListViewImageList.Images.SetKeyName(0, "file-icon.png");
            // 
            // FilterBox
            // 
            this.FilterBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FilterBox.Location = new System.Drawing.Point(99, 18);
            this.FilterBox.Name = "FilterBox";
            this.FilterBox.Size = new System.Drawing.Size(158, 22);
            this.FilterBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Filter:";
            // 
            // RefreshButton
            // 
            this.RefreshButton.Image = ((System.Drawing.Image)(resources.GetObject("RefreshButton.Image")));
            this.RefreshButton.Location = new System.Drawing.Point(12, 12);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(32, 32);
            this.RefreshButton.TabIndex = 3;
            this.RefreshButton.UseVisualStyleBackColor = true;
            // 
            // DatabaseViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 302);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FilterBox);
            this.Controls.Add(this.FilesListView);
            this.MinimumSize = new System.Drawing.Size(219, 349);
            this.Name = "DatabaseViewForm";
            this.Text = "View Database";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView FilesListView;
        private System.Windows.Forms.TextBox FilterBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader FileNameHeader;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.ImageList ListViewImageList;
    }
}